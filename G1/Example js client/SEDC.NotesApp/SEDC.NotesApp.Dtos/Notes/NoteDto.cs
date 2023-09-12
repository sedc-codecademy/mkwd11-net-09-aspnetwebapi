using SEDC.NotesApp.Domain.Enums;

namespace SEDC.NotesApp.Dtos.Notes
{
    public class NoteDto // DTO stands for Data Transfer Object
    {
        public string Text { get; set; }
        public string Color { get; set; }
        public TagEnum Tag { get; set; }
        public string UserFullname { get; set; }
    }
}
