using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Implementation;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementation;
using SEDC.NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Tests
{
    [TestClass]
    public class NoteServiceDbTests
    {
        private INoteRepository _notesRepository;
        private IUserRepository _userRepository;
        private INoteService _notesService;
        private NotesAppDbContext _context;

        [TestInitialize]
        public void NotesTestInitialize()
        {
            var builder = new DbContextOptionsBuilder<NotesAppDbContext>()
                .UseSqlServer("Server=.\\sqlExpress;Database=noteappdbtest;Trusted_Connection=True;Encrypt=False");

            _context = new NotesAppDbContext(builder.Options);

            //The database will reset all the time the tests are ran
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();

            _notesRepository = new NoteRepository(_context);
            _userRepository = new UserRepository(_context);
            _notesService = new NoteService(_notesRepository, _userRepository);
        }

        [TestMethod]
        public void GetByIdAsync_GetNoteById_True()
        {
            // Arrange
            var note = new Note()
            {
                Priority = PriorityEnum.Medium,
                Tag = TagEnum.SocialLife,
                Text = "HE HE",
                User = new User()
                {
                    FirstName = "test",
                    LastName = "test",
                    Password = "Test123!",
                    Username = "testuser",
                    Age = 33
                }
            };

            _context.Add(note);
            _context.SaveChanges();

            // Act
            var result = _notesRepository.GetById(2);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAll_CountIsZero()
        {
            var notes = _notesRepository.GetAll();

            Assert.AreEqual(0, notes.Count);
        }

    }
}
