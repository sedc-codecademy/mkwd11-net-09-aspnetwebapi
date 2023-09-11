using Profiles.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.DAL.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);

        User? GetByEmail(string email);

        User GetUser(int id);
    }
}
