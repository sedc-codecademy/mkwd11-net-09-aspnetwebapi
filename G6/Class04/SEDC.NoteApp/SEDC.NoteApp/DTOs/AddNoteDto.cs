using SEDC.NoteApp.Models.Enums;

namespace SEDC.NoteApp.DTOs
{
    public class AddNoteDto
    {
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public int UserId { get; set; }
        public List<int> TagsIds { get; set; }
    }
}
