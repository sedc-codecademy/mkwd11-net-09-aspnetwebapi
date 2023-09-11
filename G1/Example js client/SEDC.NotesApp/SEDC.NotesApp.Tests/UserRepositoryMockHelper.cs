using Moq;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.NotesApp.Tests
{
    public static class UserRepositoryMockHelper
    {
        public static IUserRepository GetMockedUserRepository(string userName, string password, int? userId)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            List<User> users = new List<User>()
            {
                new User(){
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Username = "bob007",
                    Password = hashedPassword,
                    Role = "Admin"
                }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetAll()).Returns(users);
            //we cover the scenario where GetById is called with parameter with value of userId
            userRepositoryMock.Setup(x => x.GetById(It.Is<int>(y => y == userId.Value)))
                .Returns(users.FirstOrDefault(x => x.Id == userId.Value));
            //we cover the scenario where Insert is called with any  user as parameter
            userRepositoryMock.Setup(x => x.Insert(It.IsAny<User>())).Callback((User user) =>
            {
                users.Add(user);
            });

            userRepositoryMock.Setup(x => x.Update(It.IsAny<User>())).Callback((User user) =>
            {
                User userDb = users.FirstOrDefault(x => x.Id == user.Id);
                int index = users.IndexOf(userDb);
                users[index] = user;
            });

            userRepositoryMock.Setup(x => x.Delete(It.IsAny<User>())).Callback((User user) =>
            {
                users.Remove(user);
            });

            var md5data2 = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            var hashedPassword2 = Encoding.ASCII.GetString(md5data2);
            userRepositoryMock.Setup(x => x.LoginUser(It.Is<string>(y => y == userName), It.Is<string>(y => y == password)))
                .Returns(users.FirstOrDefault(y => y.Username == userName && y.Password == hashedPassword2));


            return userRepositoryMock.Object;
        }
    }
}
