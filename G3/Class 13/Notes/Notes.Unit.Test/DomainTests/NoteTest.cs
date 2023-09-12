using Notes.Data.Domain;

namespace Notes.Unit.Test.DomainTests
{
    [TestClass]
    public class NoteTests
    {
        [TestMethod]
        public void IsOwner_ReturnsTrue_WhenPassedTheOwner()
        {
            //Arrange
            var owner = new User()
            {
                Id = 1
            };
            var note = new Note
            {
                User = owner
            };

            //Act
            var result = note.IsOwner(owner);

            //Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsOwner_ReturnsFalse_WhenPassedTheDiffrentUser()
        {
            //Arrange
            var notOwner = new User()
            {
                Id = 1
            };
            var note = new Note
            {
                User = new User
                {
                    Id = 45
                }
            };

            //Act
            var result = note.IsOwner(notOwner);

            //Assert
            Assert.IsFalse(result);
        }
    }
}