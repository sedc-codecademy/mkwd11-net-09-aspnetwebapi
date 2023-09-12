using Microsoft.IdentityModel.Tokens;
using SEDC.NoteApp.CryptoService;
using SEDC.NoteApp.CustomExceptions;
using SEDC.NoteApp.DataAccess.Abstraction;
using SEDC.NoteApp.Domain.Models;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Services.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SEDC.NoteApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Username) ||
                string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            var userFromDb = _userRepository.LoginUser(loginUserDto.Username, StringHasher.Hash(loginUserDto.Password));

            if (userFromDb == null) 
            {
                throw new UserNotFoundException("User not found!");
            }

            //Generate JWT Token
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret secret key");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(3),
                //signature configuration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[] 
                    {
                        new Claim("userFullName", $"{userFromDb.FirstName} {userFromDb.LastName}"),
                        new Claim("userId", $"{userFromDb.Id}"),
                        new Claim(ClaimTypes.Name, userFromDb.Username)
                    }
                )
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1. validation
            ValidateUser(registerUserDto);

            //2. Hash password
            var passwordHash = StringHasher.Hash(registerUserDto.Password);

            //3. create new user
            var user = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Age = registerUserDto.Age,
                Password = passwordHash
            };

            _userRepository.Add(user);
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new UserDataException("Password must match!");
            }

            if (string.IsNullOrEmpty(registerUserDto.Username) ||
                string.IsNullOrEmpty(registerUserDto.Password) ||
                string.IsNullOrEmpty(registerUserDto.ConfirmPassword))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            if (registerUserDto.Username.Length > 30)
            {
                throw new UserDataException("Username: Maximum length for username is 30 characters");
            }

            if (registerUserDto.FirstName.Length > 50)
            {
                throw new UserDataException("Maximum length for FirstName is 50 characters");
            }

            if (registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Maximum length for LastName is 50 characters");
            }

            var userFromDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userFromDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} is already in use!");
            }
        }
    }
}
