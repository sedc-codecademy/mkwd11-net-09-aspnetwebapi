namespace SEDC.NotesApp.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public virtual List<Note> Notes { get; set; }
    }
}
