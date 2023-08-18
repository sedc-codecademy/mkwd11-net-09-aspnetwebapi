namespace SEDC.NotesAppFinal.Domain.Models
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Username { get; set; } = string.Empty;

        public int Age { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
