using SEDC.NoteApp.CustomExceptions;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Services.Abstraction;
using SEDC.NoteApp.Services.Implementation;
using SEDC.NoteApp.Tests.FakeRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void Register_ValidUser_AddNewUserToDatabase()
        {
            //Arrange
            var fakeUserRepository = new FakeUserRepository();
            var userService = new UserService(fakeUserRepository);

            var registerDto = new RegisterUserDto()
            {
                FirstName = "TestName",
                LastName = "TestLastName",
                Age = 10,
                Username = "testuser",
                Password = "password",
                ConfirmPassword = "password"
            };

            //Act
            userService.RegisterUser(registerDto);

            //Assert
            var newlyRegisteredUser = fakeUserRepository.GetUserByUsername(registerDto.Username);

            Assert.IsNotNull(newlyRegisteredUser);
            Assert.AreEqual(registerDto.Username, newlyRegisteredUser.Username);
        }

        [TestMethod]
        public void Register_DifferentPasswords_ThrowsUserDataException() 
        {
            //Arrange
            var fakeUserRepository = new FakeUserRepository();
            var userService = new UserService(fakeUserRepository);

            var registerDto = new RegisterUserDto()
            {
                FirstName = "TestName",
                LastName = "TestLastName",
                Age = 10,
                Username = "testuser",
                Password = "password",
                ConfirmPassword = "anotherPassword"
            };

            //Assert  //Act
            Assert.ThrowsException<UserDataException>(() => userService.RegisterUser(registerDto));
        }
    
    }
}
