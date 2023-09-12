using SEDC.NotesApp.Domain.Enums;

namespace SEDC.NotesApp.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }// hash
        public RoleEnum Role { get; set; }
        public int Age { get; set; }
        public virtual List<Note> Notes { get; set; }
    }
}
