using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public List<Tag> Tags { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Note()
        {
            Tags = new List<Tag>();
        }
    }
}
