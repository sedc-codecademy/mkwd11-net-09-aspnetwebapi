using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class AddNoteDto
    {
        //DTO = data transfer object
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public List<int> TagIds { get; set; }
    }
}
