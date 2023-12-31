﻿using Microsoft.IdentityModel.Tokens;
using SEDC.MoviesApp.DataAccess.Interfaces;
using SEDC.MoviesApp.Domain.Domain;
using SEDC.MoviesApp.Dtos.UserDto;
using SEDC.MoviesApp.Mappers;
using SEDC.MoviesApp.Services.Interfaces;
using SEDC.MoviesApp.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace SEDC.MoviesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto Authenticate(LoginDto model)
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
                MovieList = user.MoviList.Select(movie => movie.ToMovieDto()).ToList()
            };
            return userModel;

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

        public void RegisterUser(RegisterDto registerDto)
        {
            //Validation
            if (string.IsNullOrEmpty(registerDto.FirstName))
                throw new UserException(null, registerDto.FirstName);
            if (string.IsNullOrEmpty(registerDto.LastName))
                throw new UserException();
            if (!ValidUsername(registerDto.Username))
                throw new UserException(null, registerDto.Username, "Username is already in use");
            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new UserException(null, registerDto.Username, "Password did not match!");

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(registerDto.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = new User()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Username = registerDto.Username,
                Password = hashedPassword,
                FavoriteGenre = registerDto.FavoriteGenre
            };

            _userRepository.Add(user);
        }

        private bool ValidUsername(string username)
        {
            return _userRepository.GetAll().All(x => x.Username != username);
        }
    }
}
