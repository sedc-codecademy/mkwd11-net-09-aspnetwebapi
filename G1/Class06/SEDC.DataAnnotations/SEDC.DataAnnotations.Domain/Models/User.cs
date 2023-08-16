namespace SEDC.DataAnnotations.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Username { get; set; } = string.Empty;

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
