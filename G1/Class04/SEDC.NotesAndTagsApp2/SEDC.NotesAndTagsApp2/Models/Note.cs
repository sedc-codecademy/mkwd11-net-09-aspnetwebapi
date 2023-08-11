using SEDC.NotesAndTagsApp2.Models.Enums;

namespace SEDC.NotesAndTagsApp2.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; } = string.Empty;

        public PriorityEnum Priority { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
