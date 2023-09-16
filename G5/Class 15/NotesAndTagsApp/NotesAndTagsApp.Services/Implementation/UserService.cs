using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace NotesAndTagsApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepostory;

        public UserService(IUserRepository userRepository)
        {
            _userRepostory = userRepository;
        }

        public string LoginUser(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new Exception("Username and password are required fields!");
            }

            // hash the password
            //MD5 hash algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //Test123 -> 5467821
            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginDto.Password);

            //get the bytes of the hash string 5467821 -> 2363621
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get the hash as string 2363621 -> q654klj
            string hash = Encoding.ASCII.GetString(hashBytes);

            //try to get the user
            User userDb = _userRepostory.LoginUser(loginDto.Username, hash);
            if (userDb == null)
            {
                throw new Exception("User not found");
            }

            //GENERATE JWT TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our secret secret secret secret secret secret key");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(1), // the token will be valid for one min
                //signature configuration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName", $"{userDb.Firstname} {userDb.Lastname}"),
                        new Claim(ClaimTypes.Role, userDb.Role)
                        //new Claim("role", userDb.Role)
                    }
                )
            };

            //generate token
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert to string
            return jwtSecurityTokenHandler.WriteToken(token);
        }
        public void Register(RegisterUserDto registerUserDto)
        {
            //validate user
            ValidateUser(registerUserDto);

            //hash the password
            MD5CryptoServiceProvider  mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //Test123 -> 546721
            byte[]  passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            //get the bytes of hash string 546721 -> 21346
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get the has as string 21346-> qR5Tf
            string hash = Encoding.ASCII.GetString(hashBytes);

            //create the user
            User user = new User
            {
                Firstname = registerUserDto.FirstName,
                Lastname = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Role = registerUserDto.Role,
                Password = hash
            };
            
            _userRepostory.Add(user);
        }

        public void ValidateUser (RegisterUserDto registerUserDto)
        {
            if(string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new Exception("Username and password are required!");
            }

            if(registerUserDto.Username.Length > 50)
            {
                throw new Exception("Maximum length of username is 50 characters");
            }

            if(registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new Exception("Passwords must match");
            }

            var userDb = _userRepostory.GetUserByUsername(registerUserDto.Username);
            if(userDb != null) //if there is a user with that username in db
            {
                throw new Exception($"The username {registerUserDto.Username} is already taken!");
            }
        }
    }
}
