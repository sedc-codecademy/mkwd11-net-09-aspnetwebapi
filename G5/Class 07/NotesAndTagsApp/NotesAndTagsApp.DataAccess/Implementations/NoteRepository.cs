﻿using Microsoft.EntityFrameworkCore;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementations
{
    public class NoteRepository : IRepository<Note>
    {
        private NotesAndTagsAppDbContext _notesAppDbContext;

        public NoteRepository(NotesAndTagsAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
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
            return _notesAppDbContext
                .Notes
                .Include(x => x.User) //join Notes table with Users table, to be able to access user properties
                .ToList();
        }

        public Note GetById(int id)
        {
            return _notesAppDbContext
                .Notes
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
