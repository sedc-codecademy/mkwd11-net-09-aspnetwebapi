using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
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
        private IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public UserDto LoginUser(LoginUserDto loginUserDto)
        {
            //1. validation
            if (string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new UserDataException("Username or password cannot be empty");
            }

            //2. hash data
            string stringHash = HashPassword(loginUserDto.Password);

            var user = _userRepository.LoginUser(loginUserDto.Username, stringHash);

            if(user == null)
            {
                throw new UserNotFoundException("Password or username is wrong");
            }

            //GENERATE JWT TOKEN
            string tokenString = GenerateJWT(user);

            UserDto userDto = user.ToUserDto(tokenString);

            return userDto;
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1 validation
            ValidateUser(registerUserDto);

            //2. hash data
            string stringHash = HashPassword(registerUserDto.Password);

            //3. save in database
            var user = registerUserDto.ToUserModel(stringHash);
      
            _userRepository.Add(user);
        }

        private string HashPassword(string password)
        {
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] hash = md5CryptoServiceProvider.ComputeHash(passwordBytes);

            string stringHash = Convert.ToHexString(hash);

            return stringHash;
        }

        private string GenerateJWT(User user)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //byte[] secretKeyByte = Encoding.ASCII.GetBytes("This is our secret keyss");
            byte[] secretKeyByte = Encoding.ASCII.GetBytes(_configuration["Appsettings:SecretKey"]);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleEnum), user.Role)),                   
                    })
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            string tokenString = jwtSecurityTokenHandler.WriteToken(token);

            return tokenString;

        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmationPassword))
            {
                throw new UserDataException("Username and password are required fields!");
            }
            if (registerUserDto.Username.Length > 30)
            {
                throw new UserDataException("Username: Maximum length for username is 30 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.FirstName) && registerUserDto.FirstName.Length > 50)
            {
                throw new UserDataException("Maximum length for FirstName is 50 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.LastName) && registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Maximum length for LastName is 50 characters");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmationPassword)
            {
                throw new UserDataException("Passwords must match!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} is already in use!");
            }
        }
    }

}
