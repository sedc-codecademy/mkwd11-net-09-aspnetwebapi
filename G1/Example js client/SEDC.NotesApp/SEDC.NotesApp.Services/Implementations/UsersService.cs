using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared;
using SEDC.NotesApp.Shared.CustomExceptions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private IUserRepository _userRepository;
        IOptions<AppSettings> _options;
        public UsersService(IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }
        public void Register(RegisterUserDto registerUserDto)
        {
            ValidateUser(registerUserDto);

            //use MD5 hash algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            //get the bytes from the password string
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);
            //get the hash
            byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            //get the string hash
            string hashedPasword = Encoding.ASCII.GetString(passwordHash);


            //Test123! - > 56945463 -> MD5 ->747534985 -> (hash) fe4rt

            User newUser = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Role = registerUserDto.Role,
                Password = hashedPasword //we send the hashed password value to the db
            };
            _userRepository.Insert(newUser);
        }
        public string Login(LoginDto loginDto)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(loginDto.Password));
            string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

            User userDb = _userRepository.LoginUser(loginDto.Username, hashedPassword);
            if (userDb == null)
            {
                throw new ResourceNotFoundException($"Could not login user {loginDto.Username}");
            }

            //GENERATE JWT TOKEN

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2), // the token will be valid for one hour
                //signature configuration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                        new Claim(ClaimTypes.Role, userDb.Role)
                    }

                )

            };
            //generate token with the configuration
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert it to string
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserException("Username and password are required fields!");
            }
            if (string.IsNullOrEmpty(registerUserDto.Role))
            {
                throw new UserException("Role is a required field!");
            }
            if (registerUserDto.Username.Length > 30)
            {
                throw new UserException("Username can contain maximum 30 characters!");
            }
            if (registerUserDto.FirstName.Length > 50 || registerUserDto.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }
            if (!IsUserNameUnique(registerUserDto.Username))
            {
                throw new UserException("A user with this username already exists!");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(registerUserDto.Password))
            {
                throw new UserException("The password is not complex enough!");
            }
        }

        private bool IsUserNameUnique(string username)
        {
            return _userRepository.GetUserByUsername(username) == null;
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }

        
    }
}
