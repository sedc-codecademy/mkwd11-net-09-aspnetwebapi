using SEDC.NotesAppFinal.Domain.Enums;

namespace SEDC.NotesAppFinal.DTOs.NoteDTOs
{
    public class CreateNoteDto
    {
        public string Text { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public Tag Tag { get; set; }

        public int UserId { get; set; }
    }
}
