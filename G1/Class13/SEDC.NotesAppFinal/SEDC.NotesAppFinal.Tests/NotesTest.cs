using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppFinal.DataAccess;
using SEDC.NotesAppFinal.DataAccess.Implementations;
using SEDC.NotesAppFinal.DataAccess.Interfaces;
using SEDC.NotesAppFinal.Domain.Models;
using SEDC.NotesAppFinal.DTOs.NoteDTOs;
using SEDC.NotesAppFinal.Services.Implementations;
using SEDC.NotesAppFinal.Services.Interfaces;

namespace SEDC.NotesAppFinal.Tests
{
    [TestClass]
    public class NotesTest
    {
        private IRepository<Note> _notesRepository;
        private INotesService _notesService;
        private NotesDbContext _context;

        [TestInitialize]
        public void NotesTestInitialize()
        {
            var builder = new DbContextOptionsBuilder<NotesDbContext>()
                .UseSqlServer(@"Data Source=(localdb)\LocalTest;Database=NotesAppFinalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            _context = new NotesDbContext(builder.Options);

            //The database will reset all the time the tests are ran
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();

            _notesRepository = new NotesRepository(_context);
            _notesService = new NotesService(_notesRepository);
        }

        [TestMethod]
        public async Task GetByIdAsync_GetNoteById_True()
        {
            // Arrange
            var note = new Note()
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "HE HE",
                User = new User()
                {
                    FirstName = "Bojan",
                    LastName = "Damcevski",
                    Password = "Test123!",
                    Username = "BoksanBatuleto",
                    Age = 25
                }
            };

            _context.Add(note);
            _context.SaveChanges();

            // Act
            var result = await _notesRepository.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task GetAllAsync_True()
        {
            // Arrange
            var note1 = new Note()
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "HE HE",
                User = new User()
                {
                    FirstName = "Bojan",
                    LastName = "Damcevski",
                    Password = "Test123!",
                    Username = "BoksanBatuleto",
                    Age = 25
                }
            };

            var note2 = new Note()
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "HE HE",
                User = new User()
                {
                    FirstName = "Bojan",
                    LastName = "Damcevski",
                    Password = "Test123!",
                    Username = "BoksanBatuleto2",
                    Age = 25
                }
            };

            _context.Add(note1);
            _context.Add(note2);
            _context.SaveChanges();

            // Act
            var result = await _notesRepository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetNotesAsync_FromService_True()
        {
            // Arrange
            var note1 = new Note()
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "HE HE",
                User = new User()
                {
                    FirstName = "Bojan",
                    LastName = "Damcevski",
                    Password = "Test123!",
                    Username = "BoksanBatuleto",
                    Age = 25
                }
            };

            var note2 = new Note()
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "HE HE",
                User = new User()
                {
                    FirstName = "Bojan",
                    LastName = "Damcevski",
                    Password = "Test123!",
                    Username = "BoksanBatuleto2",
                    Age = 25
                }
            };

            _context.Add(note1);
            _context.Add(note2);
            _context.SaveChanges();

            // Act
            var result = await _notesService.GetAllNotesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetNotesAsync_WithIdFromService_Exception()
        {
            // Arrange
            var note = new Note()
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "HE HE",
                User = new User()
                {
                    FirstName = "Bojan",
                    LastName = "Damcevski",
                    Password = "Test123!",
                    Username = "BoksanBatuleto",
                    Age = 25
                }
            };

            _context.Add(note);
            _context.SaveChanges();

            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _notesService.GetNoteAsync(1000));
        }

        [TestMethod]
        public async Task CreateNoteAsync_FromService_False()
        {
            // Arrange
            var user = new User()
            {
                FirstName = "Bojan",
                LastName = "Damcevski",
                Password = "Test123!",
                Username = "BoksanBatuleto",
                Age = 25
            };

            _context.Add(user);
            _context.SaveChanges();

            var note = new CreateNoteDto()
            {
                Priority = Domain.Enums.Priority.Medium,
                Tag = Domain.Enums.Tag.SocialLife,
                Text = "HE HE",
                UserId = 1
            };

            // Act
            await _notesService.CreateNoteAsync(note);
            var result = await _notesRepository.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }
    }
}
