using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SEDC.MoviesApp.DataAccess;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Domain.Enums;
using SEDC.MoviesApp.Dtos.Users;
using SEDC.MoviesApp.Mappers;
using SEDC.MoviesApp.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace SEDC.MoviesApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public UserDto Login(LoginUserDto loginUserDto)
        {
            var hashedPAssword = GenerateHashPassword(loginUserDto.Password);

            var user = _userRepository.Login(loginUserDto.UserName, hashedPAssword);
            if(user == null)
            {
                throw new UserNotFoundException("A user with this login information does not exist");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            var tokenDescripor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleEnum), user.Role))
                        }
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescripor);

            var moviesDto = user.Movies.Select(movie => movie.ToDto()).ToList();

            var userModel = new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Movies = moviesDto,
                Token = tokenHandler.WriteToken(token),
            };

            return userModel;
        }

        public void Register(RegisterUserDto registerUserDto)
        {
           
            if(string.IsNullOrEmpty(registerUserDto.FirstName))
            {
                throw new UserException("FirstName is requiered");
            }
            if (string.IsNullOrEmpty(registerUserDto.LastName))
            {
                throw new UserException("LastName is requiered");
            }
            if (string.IsNullOrEmpty(registerUserDto.UserName))
            {
                throw new UserException("Username is requiered");
            }
            if(!ValidateUserName(registerUserDto.UserName))
            {
                throw new UserException("Username is already in use");
            }
            if(registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new UserException("Passwrods must match");
            }

            var hashedPAssword = GenerateHashPassword(registerUserDto.Password);

            var user = new User()
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                UserName = registerUserDto.UserName,
                Password = hashedPAssword,
                Role = RoleEnum.User,
            };

            _userRepository.Add(user);
        }

        private bool ValidateUserName(string username)
        {
            var user = _userRepository.GetUserByUserName(username);
            if(user is null)
            {
                return true;
            }

            return false;
        }

        private string GenerateHashPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPAssword = Encoding.ASCII.GetString(md5data);

            return hashedPAssword;
        }
    }
}
