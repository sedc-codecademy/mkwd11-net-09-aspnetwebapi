using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared;
using SEDC.NotesApp.Shared.CustomExceptions;
using System;

namespace SEDC.NotesApp.Tests
{
    [TestClass]
    public class UsersServiceTests
    {
        private readonly IUsersService _usersService;

        public UsersServiceTests()
        {
            IOptions<AppSettings> options = Options.Create<AppSettings>(new AppSettings()
            {
                SecretKey = "Our test secret key"
            });
            _usersService = new UsersService(new FakeUserRepository(), options);
        }

        [TestMethod]
        public void Login_should_succeed_and_return_token()
        {
            //Arrange
            string username = "bob007";
            string password = "123456sedc";

            //Act 
            string result = _usersService.Login(new LoginDto { Username = username, Password = password });

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        public void Login_should_throw_exception_on_invlaid_credentials()
        {
            //Arrange
            string username = "testUser";
            string password = "123456sedc";

            //Act and Assert
            Assert.ThrowsException<ResourceNotFoundException>(() => _usersService.Login(new LoginDto()
            {
                Username = username,
                Password = password
            }));

        }

        [TestMethod]
        public void Register_should_add_new_user()
        {
            //Arrange
            RegisterUserDto registerUserDto = new RegisterUserDto()
            {
                FirstName = "test",
                LastName = "Test",
                Username = "testUser",
                Password = "Test123!",
                ConfirmedPassword = "Test123!",
                Role = "User"
            };
            //Act
            _usersService.Register(registerUserDto);

            //Assert
            string resultToken = _usersService.Login(new LoginDto()
            {
                Username = "testUser",
                Password = "Test123!"
            });
            Assert.IsNotNull(resultToken);
            Assert.IsTrue(resultToken != string.Empty);
        }

        [ExpectedException(typeof(UserException))]
        [TestMethod]
        public void Register_should_fail_on_validation_of_same_passwords()
        {
            //Arrange
            RegisterUserDto registerUserDto = new RegisterUserDto()
            {
                FirstName = "test",
                LastName = "Test",
                Username = "testUser222",
                Password = "Test123!",
                ConfirmedPassword = "Test12333333!",
                Role = "User"
            };
            //Act and Assert
            _usersService.Register(registerUserDto);

        }
    }
}
