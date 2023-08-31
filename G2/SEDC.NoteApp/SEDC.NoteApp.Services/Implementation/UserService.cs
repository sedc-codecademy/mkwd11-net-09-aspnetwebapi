using SEDC.NoteApp.CustomExceptions;
using SEDC.NoteApp.DataAccess.Abstraction;
using SEDC.NoteApp.Domain.Models;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Services.Abstraction;
using System.Text;
using XSystem.Security.Cryptography;

namespace SEDC.NoteApp.Services.Implementation
{
    // XAct.Core.PCL => nuget for hashing
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

            var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginUserDto.Password);

            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string passwordHash = Encoding.ASCII.GetString(hashedBytes);

            var userFromDb = _userRepository.LoginUser(loginUserDto.Username, passwordHash);

            if (userFromDb == null) 
            {
                throw new UserNotFoundException("User not found!");
            }

            // return token 
            return "fake token";
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1. validation
            ValidateUser(registerUserDto);

            //2. Hash password
            var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string passwordHash = Encoding.ASCII.GetString(hashedBytes);

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
