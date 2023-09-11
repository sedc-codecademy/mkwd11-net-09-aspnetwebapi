using Microsoft.IdentityModel.Tokens;
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

        public UserDto Authenticate(LoginDto loginDto)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(loginDto.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username.ToLower() == loginDto.Username.ToLower() && x.Password == loginDto.Password);

            if (user == null) throw new UserException(null, loginDto.Username, "Username not found");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Our secret key secret key secret key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}" ),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userModel = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token),
                MovieList = user.MoviList.Select(x=>x.ToMovieDto()).ToList()

            };

            return userModel;


        }

        public string LoginUser(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
                throw new UserException(null, loginDto.Username, "Username and password are required fields!");

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginDto.Password);
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            User userDb = _userRepository.LoginUser(loginDto.Username, hashPassword);
            if(userDb == null)
                throw new UserException(null, loginDto.Username, "Username not found!");

            //TOKEN
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our secret secret key key");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{userDb.FirstName} {userDb.LastName}" ),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString())
                    }
                )
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);

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
