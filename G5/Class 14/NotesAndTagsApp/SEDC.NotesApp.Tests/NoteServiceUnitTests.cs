using Moq;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Services.Implementation;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTagsApp.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Tests
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
        public void GetAllNotes_Should_Return_NotesDTO()
        {
            //Arrange
            List<Note> notes = new List<Note>()
            {
                new Note
                {
                    Id = 1,
                    Tag = NotesAndTagsApp.Domain.Enums.TagEnum.Exercise,
                    Text = "Do the homework",
                    Priority = NotesAndTagsApp.Domain.Enums.PriorityEnum.High,
                    UserId = 1,
                    User = new User
                    {
                        Id = 1,
                        Firstname = "Mitko",
                        Lastname = "Veljusliev",
                        Username = "mitkov"
                    }
                },
                new Note
                {
                    Id = 2,
                    Tag = NotesAndTagsApp.Domain.Enums.TagEnum.Health,
                    Text = "Drink water",
                    Priority = NotesAndTagsApp.Domain.Enums.PriorityEnum.Medium,
                    UserId = 2,
                    User = new User
                    {
                        Id = 2,
                        Firstname = "Mice",
                        Lastname = "Karajov",
                        Username = "micek"
                    }
                }
            };

            _noteRepository.Setup(x=>x.GetAll()).Returns(notes);

            //Act
            List<NoteDto> resultNoteDto = _noteService.GetAllNotes();

            //Assert

            Assert.AreEqual(2, resultNoteDto.Count);
            Assert.AreEqual("Do the homework", resultNoteDto.First().Text);
            Assert.AreEqual("Drink water", resultNoteDto.Last().Text);
            Assert.AreEqual("UserFullname", resultNoteDto.Last().UserFullName);
        }

        [TestMethod]
        public void GetAll_Should_RetrunEmptyList()
        {
            //Arrange
            _noteRepository.Setup(x => x.GetAll()).Returns(new List<Note>());

            //Act
            List<NoteDto> resultNoteDto = _noteService.GetAllNotes();

            //Assert
            Assert.AreEqual(0, resultNoteDto.Count);

        }

        [TestMethod]
        public void GetById_Should_NoteNotFound()
        {
            //Arrange
            int id = 20;
            _noteRepository.Setup(x => x.GetById(id)).Returns(null as Note);

            //Act and assert
            var exception = Assert.ThrowsException<NoteNotFoundException>(() => _noteService.GetById(id));

            //Assert
            Assert.AreEqual($"Note with id {id} was not found", exception.Message);

        }


        //Homework TestMethod for AddNote and UpdateNote
    }
}
