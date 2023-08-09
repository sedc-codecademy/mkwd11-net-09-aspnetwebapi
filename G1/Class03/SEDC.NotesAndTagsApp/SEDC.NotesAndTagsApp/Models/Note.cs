using SEDC.NotesAndTagsApp.Models.Enums;

namespace SEDC.NotesAndTagsApp.Models
{
    public class Note
    {
        public string Text { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
