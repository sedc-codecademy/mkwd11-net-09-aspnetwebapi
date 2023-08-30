using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos;
using SEDC.NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace SEDC.NotesApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
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

            var user = _userRepository.GetAll().Where(x => x.Username == loginUserDto.Username && x.Password == stringHash).FirstOrDefault();

            if(user == null)
            {
                throw new ArgumentException("Password or username is wrong");
            }

            return $"{user.FirstName} {user.LastName}";
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
