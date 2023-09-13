using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Tests.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private List<User> users;

        public FakeUserRepository()
        {
            users = new List<User>()
            {
                new User()
                {
                   Id = 1,
                   Firstname = "Mitko",
                   Lastname = "Veljusliev",
                   Username = "mitkov"
                },
                new User()
                {
                   Id = 2,
                   Firstname = "Mice",
                   Lastname = "Karajov",
                   Username = "micek"
                }
            };
        }

        public void Add(User entity)
        {
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
            return users.FirstOrDefault(user => user.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User LoginUser(string username, string hashedPassword)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            users[users.IndexOf(entity)] = entity;
        }
    }
}
