﻿using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.DataAccess.Interfaces;
using SEDC.MoviesApp.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesDbContext _context;
        public UserRepository(MoviesDbContext context)
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
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(x => x.MoviList);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }

        public User LoginUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
    }
}
