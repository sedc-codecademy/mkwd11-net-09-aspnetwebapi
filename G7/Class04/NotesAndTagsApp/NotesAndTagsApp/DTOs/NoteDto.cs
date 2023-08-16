using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class NoteDto
    {
        //DTO = data transfer object
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public List<string> TagNames { get; set; }
    }
}
