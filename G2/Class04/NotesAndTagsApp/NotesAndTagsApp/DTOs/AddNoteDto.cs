using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class AddNoteDto
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
