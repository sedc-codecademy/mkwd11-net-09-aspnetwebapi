namespace Notes.Api.Domain
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
