using Microsoft.Identity.Client;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
