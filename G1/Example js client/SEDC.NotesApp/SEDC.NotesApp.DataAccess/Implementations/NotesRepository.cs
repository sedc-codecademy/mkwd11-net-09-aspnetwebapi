using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.NotesApp.DataAccess.Implementations
{
    public class NotesRepository : IRepository<Note>
    {
        private NotesAppDbContext _notesAppDbContext;
        public NotesRepository(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }
        public void Delete(Note entity)
        {
            _notesAppDbContext.Notes.Remove(entity);
            _notesAppDbContext.SaveChanges();
        }

        public List<Note> GetAll()
        {
            return _notesAppDbContext
                .Notes
                .Include(x => x.User) //join table Notes with table Users
                .ToList();
        }

        public Note GetById(int id)
        {
            return _notesAppDbContext
                .Notes
                .Include(x => x.User) //join table Notes with table Users
                .FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Note entity)
        {
            _notesAppDbContext.Notes.Add(entity);
            _notesAppDbContext.SaveChanges(); //request to DB
        }

        public void Update(Note entity)
        {
            _notesAppDbContext.Notes.Update(entity);
            _notesAppDbContext.SaveChanges(); //call to db
        }
    }
}
