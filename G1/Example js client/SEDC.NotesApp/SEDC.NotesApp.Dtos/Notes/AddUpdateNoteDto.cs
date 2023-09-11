using SEDC.NotesApp.Domain.Enums;

namespace SEDC.NotesApp.Dtos.Notes
{
    public class AddUpdateNoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public TagEnum Tag { get; set; }
        public int UserId { get; set; }
    }
}
