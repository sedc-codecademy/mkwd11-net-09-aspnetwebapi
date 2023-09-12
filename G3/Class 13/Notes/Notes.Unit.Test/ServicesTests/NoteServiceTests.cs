using AutoMapper;
using Moq;
using Notes.Data.Domain;
using Notes.Data.Repositories;
using Notes.Services.Models;
using Notes.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Unit.Test.ServicesTests
{
    [TestClass]
    public class NoteServiceTests
    {
        private NoteService service;
        private Mock<INotesRepository> noteRepository = new Mock<INotesRepository>();
        private Mock<IMapper> mapper = new Mock<IMapper>();
        private Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
        public NoteServiceTests()
        {
            service = new NoteService(noteRepository.Object, mapper.Object, userRepository.Object);
        }


        [TestMethod] //MethodName_ExpectedOutCome_WhenCondition
        public void CreateNote_ShouldCreateAndReturnNote_WhenUserExits()
        {
            // Arrange
            var user = new Data.Domain.User();
            userRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);
            mapper.Setup(x => x.Map<NoteModel>(It.IsAny<Note>())).Returns(new NoteModel());
            var create = new CreateNoteModel
            {
                Description = "asd"
            };
            // Act
            service.Create(new FakeUser(1, ""), create);
            // Assert

            noteRepository.Verify(x => x.Create(It.IsAny<Note>()), Times.Once);

        }

    }
}
