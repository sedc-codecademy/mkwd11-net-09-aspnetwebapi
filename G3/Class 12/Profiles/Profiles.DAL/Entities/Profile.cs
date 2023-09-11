namespace Profiles.DAL.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int YearOfBirth { get; set; }

        public string? ImageUrl { get; set; } = string.Empty;

        public ICollection<Connection> ConnectionTos { get; set; } = new List<Connection>();
    
        public ICollection<Connection> ConnectionFroms { get; set; } = new List<Connection>();

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
