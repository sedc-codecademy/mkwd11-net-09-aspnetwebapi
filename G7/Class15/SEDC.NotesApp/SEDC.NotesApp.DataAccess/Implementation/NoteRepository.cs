using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess.Implementation
{
    public class NoteRepository : IRepository<Note>, INoteRepository
    {
        private NotesAppDbContext _dbContext;

        public NoteRepository(NotesAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Note entity)
        {
            _dbContext.Notes.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Note entity)
        {
            var note = GetById(entity.Id);

            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();
        }

        public List<Note> GetAll()
        {
            return _dbContext.Notes.Include(x => x.User).ToList();

            //Select * 
            //From Notes n 
            //Inner Join Users u ON n.UserId = u.Id
        }

        public Note GetById(int id)
        {
            var note = _dbContext.Notes.Include(x => x.User).FirstOrDefault(x => x.Id == id);

            if (note == null)
                throw new KeyNotFoundException($"Note with id {id} is not found");

            return note;
        }

        public Note GetByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            _dbContext.Notes.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
