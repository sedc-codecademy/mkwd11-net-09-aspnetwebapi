namespace Notes.Data.Domain
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    
    
        public bool IsOwner(User user)
        {
            return User.Id == user.Id;
        }
    }
}
