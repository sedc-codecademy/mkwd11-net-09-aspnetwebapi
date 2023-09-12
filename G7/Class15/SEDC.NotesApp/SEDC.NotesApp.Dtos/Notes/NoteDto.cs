using SEDC.NotesApp.Domain.Enums;

namespace SEDC.NotesApp.Dtos.Notes
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public TagEnum Tag { get; set; }
        public string UserFullName { get; set; }
    }
}