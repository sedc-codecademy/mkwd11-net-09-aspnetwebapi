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

        public async Task CreateAsync(Note entity)
        {
            await _context.Notes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Note noteDb = await GetByIdAsync(id);

            _context.Remove(noteDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _context.Notes
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Note entity)
        {
            _context.Notes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
