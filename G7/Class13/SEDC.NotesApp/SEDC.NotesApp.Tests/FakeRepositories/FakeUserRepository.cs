using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace SEDC.NotesApp.Tests.FakeRepositories
{
    public class FakeUserRepository : IRepository<User>
    {
        private List<User> _users;

        public FakeUserRepository()
        {
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes("pas123");

            byte[] hash = md5CryptoServiceProvider.ComputeHash(passwordBytes);
            string stringHash = Convert.ToHexString(hash);

            _users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Age = 22,
                    Username = "bob22",
                    Password = stringHash,
                }
            };
        }

        public void Add(User entity)
        {
            _users.Add(entity);
        }

        public void Delete(User entity)
        {
            _users.Remove(entity);
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public User GetByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            _users[_users.IndexOf(entity)] = entity;
        }
    }
}
