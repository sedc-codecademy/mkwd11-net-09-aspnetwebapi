using NotesAndTagsAppG5.Models.Enums;

namespace NotesAndTagsAppG5.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }

        public List<Tag> Tags { get; set; } 

        public Priority Priority { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Note() {
            Tags = new List<Tag>();
        }
    }
}
