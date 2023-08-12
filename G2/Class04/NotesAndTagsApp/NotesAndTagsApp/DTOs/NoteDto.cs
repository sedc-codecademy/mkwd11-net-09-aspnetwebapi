using NotesAndTagsApp.Models;
using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class NoteDto
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public string User { get; set; }
        public List<string> Tags { get; set; }
    }
}
