using Profiles.DAL.Data;
using Profiles.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.DAL.Repositories
{
    public class UserRepository
        : IUserRepository
    {
        private readonly ProfileDbContext profileDbContext;

        public UserRepository(ProfileDbContext profileDbContext)
        {
            this.profileDbContext = profileDbContext;
        }
        public void Create(User user)
        {
            profileDbContext.Add(user);
            profileDbContext.SaveChanges();
        }

        public User? GetByEmail(string email)
        {
            return profileDbContext.User.FirstOrDefault(x => x.Email == email);
        }

        public User GetUser(int id)
        {
            return profileDbContext.User.First(x => x.Id == id);
        }
    }
}
