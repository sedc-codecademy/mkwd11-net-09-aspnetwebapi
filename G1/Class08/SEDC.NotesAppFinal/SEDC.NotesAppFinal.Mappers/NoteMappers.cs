namespace SEDC.NotesAppFinal.Mappers
{
    using SEDC.NotesAppFinal.Domain.Models;
    using SEDC.NotesAppFinal.DTOs.NoteDTOs;
    using SEDC.NotesAppFinal.DTOs.UserDtos;

    public static class NoteMappers
    {
        public static NoteDto MapToNoteDto(this Note note)
        {
            return new NoteDto()
            {
                Text = note.Text,
                Priority = note.Priority,
                Tag = note.Tag,
                User = note.User == null ? new UserDto() : note.User.MapToUserDto()
            };
        }
    }
}
