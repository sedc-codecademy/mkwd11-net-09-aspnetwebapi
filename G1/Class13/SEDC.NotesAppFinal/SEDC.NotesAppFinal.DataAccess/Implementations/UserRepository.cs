using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppFinal.DataAccess.Interfaces;
using SEDC.NotesAppFinal.Domain.Models;

namespace SEDC.NotesAppFinal.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly NotesDbContext _notesAppDbContext;

        public UserRepository(NotesDbContext _notesAppDbContext)
        {
            this._notesAppDbContext = _notesAppDbContext;
        }

        public async Task CreateAsync(User entity)
        {
            await _notesAppDbContext.Users.AddAsync(entity);
            await _notesAppDbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _notesAppDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _notesAppDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<User> LoginUser(string username, string hashedPassword)
        {
            return await _notesAppDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == hashedPassword);
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
