using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.DataAccess.EFRepositories
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly NotesAppDbContext _context;

        public NoteRepository(NotesAppDbContext context)
        {
            _context = context;
        }

        public void Add(Note entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Note entity)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            return _context
                .Notes
                .Include(x => x.User) //join with table Users
                .ToList();
        }

        public Note GetById(int id)
        {
            return _context.Notes
                 .Include(x => x.User)
                 .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
