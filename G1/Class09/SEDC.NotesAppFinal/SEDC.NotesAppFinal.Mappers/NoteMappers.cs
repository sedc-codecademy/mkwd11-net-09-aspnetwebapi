using SEDC.NotesAppFinal.Domain.Models;
using SEDC.NotesAppFinal.DTOs.NoteDTOs;

namespace SEDC.NotesAppFinal.Mappers
{
    public static class NoteMappers
    {
        public static NoteDto MapToNoteDto(this Note note)
        {
            return new NoteDto()
            {
                Text = note.Text,
                Priority = note.Priority,
                Tag = note.Tag,
                User = note.User.MapToUserDto()
            };
        }
    }
}
