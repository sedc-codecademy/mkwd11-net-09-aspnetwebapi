using NotesAndTagsAppG5.Models.Enums;

namespace NotesAndTagsAppG5.Models
{
    public class Note
    {
        public string Text { get; set; }

        public List<Tag> Tags { get; set; } 

        public Priority Priority { get; set; }
    }
}
