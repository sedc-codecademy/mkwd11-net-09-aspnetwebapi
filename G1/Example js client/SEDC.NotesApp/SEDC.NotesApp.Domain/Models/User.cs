using System.Collections.Generic;

namespace SEDC.NotesApp.Domain.Models
{
    public class User : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }
        public List<Note> Notes { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
    }
}
