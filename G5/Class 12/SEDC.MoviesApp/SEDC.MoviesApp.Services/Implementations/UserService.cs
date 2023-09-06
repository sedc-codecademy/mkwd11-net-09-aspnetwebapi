using Microsoft.Extensions.Options;
using SEDC.MoviesApp.DataAccess;
using SEDC.MoviesApp.Domain.Domain;
using SEDC.MoviesApp.Dtos;
using SEDC.MoviesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using SEDC.MoviesApp.Shared;
using SEDC.MoviesApp.Mappers;

namespace SEDC.MoviesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserDto Authenticate(LoginModelDto model)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);
            var user = _userRepository.GetAll().SingleOrDefault(x =>
                x.Username == model.Username && x.Password == hashedPassword);
            if (user == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Our very secret secret key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userModel = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token),
                MovieList = user.MovieList.Select(movie => movie.ToMovieDto()).ToList()
            };
            return userModel;

        }

        public string LoginUser(LoginModelDto loginDto)
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
            User userDb = _userRepository.LoginUser(loginDto.Username, hash);
            if (userDb == null)
            {
                throw new Exception("User not found");
            }

            //GENERATE JWT TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret secret key");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1), // the token will be valid for one hour
                //signature configuration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.Name, $"{userDb.FirstName} {userDb.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString())
                    }
                )
            };

            //generate token
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert to string
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public void Register(RegisterModelDto model)
        {
            // Validations
            if (string.IsNullOrEmpty(model.FirstName))
                throw new UserException(null, model.Username,
                    "First name is required");
            if (string.IsNullOrEmpty(model.LastName))
                throw new UserException(null, model.Username,
                    "Last name is required");
            if (!ValidUsername(model.Username))
                throw new UserException(null, model.Username,
                    "Username is already in use");
            if (model.Password != model.ConfirmPassword)
                throw new UserException(null, model.Username,
                    "Passwords did not match!");

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(model.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = hashedPassword
            };

            _userRepository.Add(user);


        }

        private bool ValidUsername(string username)
        {
            return _userRepository.GetAll().All(x => x.Username != username);
        }
    }
}
