using SEDC.NotesAppFinal.Domain.Enums;
using SEDC.NotesAppFinal.DTOs.UserDtos;

namespace SEDC.NotesAppFinal.DTOs.NoteDTOs
{
    public class NoteDto
    {
        public string Text { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public Tag Tag { get; set; }

        public UserDto User { get; set; }
    }
}
