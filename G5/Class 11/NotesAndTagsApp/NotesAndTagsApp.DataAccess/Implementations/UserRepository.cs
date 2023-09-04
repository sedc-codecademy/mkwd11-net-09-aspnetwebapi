using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {

        private NotesAndTagsAppDbContext _context; //DI

        public UserRepository(NotesAndTagsAppDbContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
           _context.Users.Add(entity);
           _context.SaveChanges();
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
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
