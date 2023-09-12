using Moq;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Services.Tests
{
    [TestClass]
    public class NoteServiceUnitTests
    {
        private readonly INoteService _noteService;
        private readonly Mock<IRepository<Note>> _noteRepository;
        private readonly Mock<IUserRepository> _userRepository;

        public NoteServiceUnitTests()
        {
            _noteRepository = new Mock<IRepository<Note>>();
            _userRepository = new Mock<IUserRepository>();

            _noteService = new NoteService(_noteRepository.Object, _userRepository.Object);
        }

        [TestMethod]
        public void GetAll_Should_Return_NotesDtos()
        {
            //Arrange
            List<Note> notes = new List<Note>()
            {
                new Note
                {
                    Id = 1,
                    Tag = Domain.Enums.Tag.Work,
                    Text = "Do the homework",
                    Priority = Domain.Enums.Priority.High,
                    UserId = 1,
                    User = new User {
                        Id = 1,
                        FirstName = "Test1",
                        LastName = "User1"

                    }
                },
                new Note
                {
                    Id = 2,
                    Tag = Domain.Enums.Tag.Health,
                    Text = "Drink water",
                    Priority = Domain.Enums.Priority.Low,
                    UserId = 2,
                    User = new User {
                        Id = 2,
                        FirstName = "Test2",
                        LastName = "User2"

                    }
                }
            };

            _noteRepository.Setup(x => x.GetAll()).Returns(notes);

            //Act
            List<NoteDto> resultNotes = _noteService.GetAll();

            //Assert
            Assert.AreEqual(2, resultNotes.Count);
            Assert.AreEqual("Do the homework", resultNotes.First().Text);
            Assert.AreEqual("Drink water", resultNotes.Last().Text);
            Assert.AreEqual("Test1 User1", resultNotes.First().UserFullName);
            Assert.AreEqual("Test2 User2", resultNotes.Last().UserFullName);
        }

        [TestMethod]
        public void GetAll_ShouldReturnEmptyList_On_EmptyList_From_Db()
        {
            //Arrange
            //simulate that no data was returned from db
            _noteRepository.Setup(x => x.GetAll()).Returns(new List<Note>());


            //Act
            List<NoteDto> resultNotes = _noteService.GetAll();

            //Assert
            Assert.AreEqual(0, resultNotes.Count);
        }

        [TestMethod]
        public void GetById_ShouldThrow_On_NoteNotFound()
        {
            //Arrange
            int id = 2;
            _noteRepository.Setup(x => x.GetById(id)).Returns(null as Note);

            //Act and assert
            var exception = Assert.ThrowsException<ResourceNotFoundException>(() =>  _noteService.GetById(id));

            //Assert
            Assert.AreEqual($"Note with id {id} was not found!", exception.Message);
        }
    }
}
