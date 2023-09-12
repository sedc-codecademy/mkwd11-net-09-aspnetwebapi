using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Tests.FakeRepositories
{
    public class FakeNotesRepository : INoteRepository
    {
        private List<Note> _notes;
        public FakeNotesRepository()
        {
            _notes = new List<Note>()
            {
                new Note()
                {
                    Id = 1,
                    Text = "Do something",
                    Priority = PriorityEnum.Low,
                    Tag = TagEnum.SocialLife,
                    UserId = 1
                },
                new Note()
                {
                    Id = 2,
                    Text = "Do something else",
                    Priority = PriorityEnum.High,
                    Tag = TagEnum.Work,
                    UserId = 1
                }
            };
        }
        public void Add(Note entity)
        {
            _notes.Add(entity);
        }

        public void Delete(Note entity)
        {
            _notes.Remove(entity);
        }

        public List<Note> GetAll()
        {
            return _notes;
        }

        public Note GetById(int id)
        {
            return _notes.FirstOrDefault(x => x.Id == id);
        }

        public Note GetByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            _notes[_notes.IndexOf(entity)] = entity;
        }
    }
}
