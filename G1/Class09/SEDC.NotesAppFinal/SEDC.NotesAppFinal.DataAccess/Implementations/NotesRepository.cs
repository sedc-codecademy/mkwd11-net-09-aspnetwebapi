using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppFinal.DataAccess.Interfaces;
using SEDC.NotesAppFinal.Domain.Models;

namespace SEDC.NotesAppFinal.DataAccess.Implementations
{
    public class NotesRepository : IRepository<Note>
    {
        private readonly NotesDbContext _context;

        public NotesRepository(NotesDbContext _context)
        {
            this._context = _context;
        }

        public Task CreateAsync(Note entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _context.Notes
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
