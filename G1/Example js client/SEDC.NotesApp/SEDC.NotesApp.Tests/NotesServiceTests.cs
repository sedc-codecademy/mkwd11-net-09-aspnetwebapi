using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.NotesApp.Tests
{
    [TestClass]
    public class NotesServiceTests
    {
        [TestMethod]
        public void GetAllNotes_should_return_valid_num_of_records()
        {
            List<Note> notes = new List<Note>()
            {
                new Note(){
                    Id = 1,
                    Text = "Don't forget to water the plant",
                    Color = "blue",
                    Tag = TagEnum.Work,
                    UserId = 1
                },
                new Note(){
                    Id = 2,
                    Text = "Drink more Tea",
                    Color = "yellow",
                    Tag = TagEnum.Health,
                    UserId = 1
                }
            };
            

            var notesMockRepository = new Mock<IRepository<Note>>();
            var usersMockRepository = UserRepositoryMockHelper.GetMockedUserRepository("bob007", "123456sedc", 1);
            notesMockRepository.Setup(x => x.GetAll()).Returns(notes);

            INotesService notesService = new NotesService(notesMockRepository.Object, usersMockRepository);

            var result = notesService.GetAllNotes();

            Assert.AreEqual(2, result.Count);
        }
    }
}
