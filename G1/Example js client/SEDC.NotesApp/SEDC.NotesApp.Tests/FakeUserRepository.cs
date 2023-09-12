using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.NotesApp.Tests
{
    public class FakeUserRepository : IUserRepository
    {
        private List<User> users;
        public FakeUserRepository()
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            users = new List<User>()
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
        }
        public void Delete(User entity)
        {
            users.Remove(entity);
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return users.FirstOrDefault(x => x.Username == username);
        }

        public void Insert(User entity)
        {
            users.Add(entity);
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);
        }

        public void Update(User entity)
        {
            User userDb = users.FirstOrDefault(x => x.Id == entity.Id);
            int index = users.IndexOf(userDb);
            users[index] = entity;
        }
    }
}
