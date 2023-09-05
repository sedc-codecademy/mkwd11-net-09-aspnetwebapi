using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.DataAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MovieAppDbContext _dbContext;

        public UserRepository(MovieAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserByUserName(string userName)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public User Login(string username, string hashedPassword)
        {
            return _dbContext.Users.Include(x => x.Movies).FirstOrDefault(x => x.UserName == username && x.Password == hashedPassword);
        }
    }
}
