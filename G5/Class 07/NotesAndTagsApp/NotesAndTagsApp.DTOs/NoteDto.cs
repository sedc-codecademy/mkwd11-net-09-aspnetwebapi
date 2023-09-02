using NotesAndTagsApp.Domain.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class NoteDto
    {
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public TagEnum Tag { get; set; }

        public string UserFullName { get; set; }
    }
}