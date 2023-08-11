namespace SEDC.NotesAndTagsApp2.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
