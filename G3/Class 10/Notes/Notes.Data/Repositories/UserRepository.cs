using Microsoft.EntityFrameworkCore;
using Notes.Data.Data;
using Notes.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(NotesDbContext notesDbContext)
            : base(notesDbContext)
        {
        }

        public User? GetByEmail(string email)
        {
            return notesDbContext.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
