using DataAccess;
using System.Security.Cryptography;

namespace Tests
{
    public class FakeUserRepository : IRepository<UserDto>
    {
        private List<UserDto> users;
        public FakeUserRepository()
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            users = new List<UserDto>()
            {
                new UserDto(){
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Username = "bob007",
                    Password = hashedPassword
                }
            };
        }
        public void Add(UserDto entity)
        {
            users.Add(entity);
        }

        public void Delete(UserDto entity)
        {
            users.Remove(entity);
        }

        public IEnumerable<UserDto> GetAll()
        {
            return users;
        }

        public UserDto GetById(int id)
        {
            return users.SingleOrDefault(x => x.Id == id);
        }

        public void Update(UserDto update)
        {
            users[users.IndexOf(update)] = update;
        }
    }
    public class FakeNoteRepository : IRepository<NoteDto>
    {
        private List<NoteDto> notes;
        public FakeNoteRepository()
        {
            notes = new List<NoteDto>()
            {
                new NoteDto(){
                    Id = 1,
                    Text = "Don't forget to water the plant",
                    Color = "blue",
                    Tag = 2,
                    UserId = 1
                },
                new NoteDto(){
                    Id = 2,
                    Text = "Drink more Tea",
                    Color = "yellow",
                    Tag = 3,
                    UserId = 1
                }
            };
        }
        public void Add(NoteDto entity)
        {
            notes.Add(entity);
        }

        public void Delete(NoteDto entity)
        {
            notes.Remove(entity);
        }

        public IEnumerable<NoteDto> GetAll()
        {
            return notes;
        }

        public NoteDto GetById(int id)
        {
            return notes.SingleOrDefault(x => x.Id == id);
        }

        public void Update(NoteDto update)
        {
            notes[notes.IndexOf(update)] = update;
        }
    }
}
