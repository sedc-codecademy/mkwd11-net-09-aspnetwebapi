using Services;
using Services.Exceptions;

namespace Tests
{
    [TestClass]
    public class NoteTests
    {
        [TestMethod]
        public void GetUserNotes_ValidUserId_AllNotesForThatUser()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
            int expectedResult = 2;
            int userId = 1;
            // Act
            IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }
        [TestMethod]
        public void GetUserNotes_InvalidUserId_null()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
            int expectedResult = 0;
            int userId = 3;
            // Act
            IEnumerable<NoteModel> result = noteService.GetUserNotes(userId);
            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }
        [TestMethod]
        public void GetNote_ValidUserId_Note()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
            int userId = 1;
            int noteId = 1;
            int expectedResult = 1;
            // Act
            NoteModel result = noteService.GetNote(noteId, userId);
            // Assert
            Assert.AreEqual(expectedResult, result.Id);
        }
        [TestMethod]
        public void GetNote_InvalidUserId_Exception()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
            int userId = 1;
            int noteId = 9;
            // Act / Assert
            Assert.ThrowsException<NoteException>(() => noteService.GetNote(noteId, userId));
        }
        [TestMethod]
        public void AddNote_ValidNote_NoteAdded()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
            int expectedResult = 3;
            NoteModel model = new NoteModel()
            {
                Id = 4,
                Text = "Test the app",
                Color = "red",
                Tag = TagType.Work,
                UserId = 1
            };
            // Act
            noteService.AddNote(model);
            // Assert
            IEnumerable<NoteModel> resultNotes = noteService.GetUserNotes(model.UserId);
            Assert.AreEqual(expectedResult, resultNotes.ToList().Count);
        }
        [TestMethod]
        public void DeleteNote_ValidNote_NoteDeleted()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeUserRepository(), new FakeNoteRepository());
            int expectedResult = 1;
            int userId = 1;
            int noteId = 1;
            // Act
            noteService.DeleteItem(noteId, userId);
            // Assert
            IEnumerable<NoteModel> resultNotes = noteService.GetUserNotes(userId);
            Assert.AreEqual(expectedResult, resultNotes.ToList().Count);
        }
    }
}
