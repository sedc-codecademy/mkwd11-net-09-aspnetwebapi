using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Implementation;
using SEDC.NotesApp.Shared.CustomExceptions;
using SEDC.NotesApp.Tests.FakeRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Tests
{
    [TestClass]
    public class NoteServiceTests
    {

        [TestMethod]
        public void GetById_ValidId_AreEquel()
        {
            //Arrange
            int id = 1;
            NoteService noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());

            //Act
            var result = noteService.GetById(id);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAll_Count()
        {
            //Arrange
            int expectedCount = 2;
            NoteService noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());

            //Act
            var notes = noteService.GetAll();

            //Assert
            Assert.AreEqual(expectedCount, notes.Count());
        }

        [ExpectedException(typeof(UserNotFoundException))]
        [TestMethod]
        public void AddNote_NoteDtoWithInvalidUserId_Exception()
        {
            //Arrange
            NoteService noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());
            AddNoteDto noteDto = new AddNoteDto()
            {
                Priority = PriorityEnum.Low,
                Tag = TagEnum.Health,
                Text = " Hello World",
                UserId = 2,
            };

            //Act
            noteService.AddNote(noteDto);
      
        }

        [TestMethod]
        public void AddNote_NoteDtoWithTooLongText_Exception()
        {
            //Arrange
            NoteService noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());
            AddNoteDto noteDto = new AddNoteDto()
            {
                Priority = PriorityEnum.Low,
                Tag = TagEnum.Health,
                Text = " Hello Worldaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                UserId = 1,
            };

            //Act
            //Assert
            Assert.ThrowsException<NoteDataException>(() => noteService.AddNote(noteDto));
        }

    }
}
