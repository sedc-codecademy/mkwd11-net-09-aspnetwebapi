using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos;
using SEDC.NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace SEDC.NotesApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new ArgumentException("Username or password cannot be empty");
            }

            //2. hash data
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginUserDto.Password);

            byte[] hash = md5CryptoServiceProvider.ComputeHash(passwordBytes);

            string stringHash = Convert.ToHexString(hash);

            var user = _userRepository.GetAll().Where(x => x.Username == loginUserDto.Username
            && x.Password == stringHash).FirstOrDefault();

            if(user == null)
            {
                throw new ArgumentException("Password or username is wrong");
            }

            //GENERATE JWT TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //byte[] secretKeyByte = Encoding.ASCII.GetBytes("This is our secret key");
            byte[] secretKeyByte = Encoding.ASCII.GetBytes(_configuration["Appsettings:SecretKey"]);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                        //form database
                        //new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Role, "Member"),
                        new Claim(ClaimTypes.Role, "Admin"),
                    })
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            string tokenString = jwtSecurityTokenHandler.WriteToken(token);

            return tokenString;
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1 validation
            if(string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmationPassword))
            {
                throw new ArgumentException("Username or password cannot be empty");
            }

            if(string.IsNullOrEmpty(registerUserDto.FirstName) || registerUserDto.FirstName.Length > 50)
            {
                throw new ArgumentException("First name cannot be empty and should have less then 50 chars");
            }
            //2. hash data
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            byte[] hash = md5CryptoServiceProvider.ComputeHash(passwordBytes);

            string stringHash = Convert.ToHexString(hash);

            //3. save in database
            var user = new User
            {
                Age = registerUserDto.Age,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Password = stringHash,
                Username = registerUserDto.Username
            };

            _userRepository.Add(user);
        }
    }
}
