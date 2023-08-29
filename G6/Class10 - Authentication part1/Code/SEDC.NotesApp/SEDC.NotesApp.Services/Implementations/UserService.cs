using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared;
using SEDC.NotesApp.Shared.Shared;
using System.Text;
using XSystem.Security.Cryptography;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            //1. validate data
            // - if we have first name and last name, they shouldn't contain more than 50 characters
            // - username must be sent and it can have max 20 characters
            // - password must be sent
            // - password and confirmed password must match
            // - username must be unique, there should be no other user with the same username
            ValidationHelper.ValidateRequiredStringColumn(registerUserDto.Username, "Username", 20);
            if(!string.IsNullOrEmpty(registerUserDto.FirstName) ) 
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.FirstName, "FirstName", 50);
            }
            if (!string.IsNullOrEmpty(registerUserDto.LastName))
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.LastName, "LastName", 50);
            }

            if(string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmedPassword))
            {
                throw new DataException("Password fields are required");
            }

            if(registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new DataException("Password do not match!");
            }

            //we need to check if there is already another user with the username from the dto in the db
            User userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if(userDb != null)
            {
                //this means that we have a user with registerUserDto.Username username in the db
                throw new DataException($"Username {registerUserDto.Username} is already in use.");
            }

            //2. add record to db
            //2.1 get domain model from the dto => RegisterUserDto -> User

            //hash the password
            //get encrypted value from the password
            //for example Test123 => hsfjdu743h

            //MD5 has algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //Test123 - get the bytes => 5678432
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            //hash the bytes => 5678432 -> 6493873
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get a string from the hashed bytes, 6493873 => qsd546f
            string hash = Encoding.ASCII.GetString(hashedBytes);

            User newUser = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                //Password = registerUserDto.Password //password must not be kept as plain text in db!!!!!
                Password = hash
            };
            _userRepository.Add(newUser);
        }
    }
}
