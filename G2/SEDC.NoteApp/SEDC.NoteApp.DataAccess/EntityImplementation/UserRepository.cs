using SEDC.NoteApp.DataAccess.Abstraction;
using SEDC.NoteApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.DataAccess.EntityImplementation
{
    public class UserRepository : IUserRepository
    {
        private readonly NoteAppDbContext _noteAppDbContext;
        public UserRepository(NoteAppDbContext noteAppDbContext)
        {
            _noteAppDbContext = noteAppDbContext;
        }

        public void Add(User entity)
        {
            _noteAppDbContext.Users.Add(entity);
            _noteAppDbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return _noteAppDbContext.Users
                        .SingleOrDefault(user => user.Id == id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            return _noteAppDbContext.Users.SingleOrDefault(user => user.Username == username);
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _noteAppDbContext.Users.FirstOrDefault(user =>
                        user.Username.ToLower() == username.ToLower() &&
                        user.Password == hashedPassword);
        }
    }
}
