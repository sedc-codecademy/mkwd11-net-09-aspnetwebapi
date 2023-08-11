using SEDC.NotesAndTagsApp2.Models.Enums;

namespace SEDC.NotesAndTagsApp2.DTOs
{
    public class NoteDto
    {
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public string User { get; set; }
        public List<string> Tags { get; set; }
    }
}
