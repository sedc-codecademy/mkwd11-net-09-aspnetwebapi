using SEDC.NoteApp.CryptoService;
using SEDC.NoteApp.DataAccess.Abstraction;
using SEDC.NoteApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Tests.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        public int userIdTracker = 2;
        private List<User> users;
        public FakeUserRepository()
        {
            users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Viktor",
                    LastName = "Jakovlev",
                    Username = "vjakovlev",
                    Age = 34,
                    Password = StringHasher.Hash("viktor123")
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Ilija",
                    LastName = "Mitev",
                    Username = "imitev",
                    Age = 35,
                    Password = StringHasher.Hash("ile123")
                }
            };
        }

        public void Add(User entity)
        {
            entity.Id = ++userIdTracker;
            users.Add(entity);
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
            return users.SingleOrDefault(user => user.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return users.SingleOrDefault(user => user.Username == username);
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return users.FirstOrDefault(user =>
                        user.Username.ToLower() == username.ToLower() &&
                        user.Password == hashedPassword);
        }

        public void Update(User entity)
        {
            users[users.IndexOf(entity)] = entity;
        }
    }
}
