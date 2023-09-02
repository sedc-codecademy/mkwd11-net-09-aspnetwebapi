using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Mappers.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared;
using SEDC.NotesApp.Shared.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        //the return type is string, because we will return the generated token
        public string Login(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new DataException("Username and password are required fields.");
            }

            string hash = GenerateHash(loginUserDto.Password);

            User userDb = _userRepository.GetUserByUsernameAndPassword(loginUserDto.Username, hash);
            if (userDb == null)
            {
                throw new ResourceNotFoundException($"Invalid login for username: {loginUserDto.Username}");
            }

            //generate JWT token that will be returned to the client
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret secretttt secret key");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(5), // the token will be valid for one hour
                //signature configuration, signing algorithm that will be used to generate hash (third part of token)
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(secretKeyBytes),
                        SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("userFullName", userDb.FirstName + " " + userDb.LastName ),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Username),
                        new Claim("userRole", "Admin") //new Claim("userRole", userDb.Role)
                    })
            };

            //generate token
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert to string
            string resultToken =  jwtSecurityTokenHandler.WriteToken(token);
            return resultToken;
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

            string hash = GenerateHash(registerUserDto.Password);

            //User newUser = new User
            //{
            //    FirstName = registerUserDto.FirstName,
            //    LastName = registerUserDto.LastName,
            //    Username = registerUserDto.Username,
            //    //Password = registerUserDto.Password //password must not be kept as plain text in db!!!!!
            //    Password = hash
            //};
            User newUser = registerUserDto.ToUser(hash);
            _userRepository.Add(newUser);
        }


        private static string GenerateHash(string password)
        {
            //MD5 has algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //Test123 - get the bytes => 5678432
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            //hash the bytes => 5678432 -> 6493873
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get a string from the hashed bytes, 6493873 => qsd546f
            return Encoding.ASCII.GetString(hashedBytes);
        }
    }
}
