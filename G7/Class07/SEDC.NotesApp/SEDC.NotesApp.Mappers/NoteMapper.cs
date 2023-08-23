using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos;

namespace SEDC.NotesApp.Mappers
{
    public static class NoteMapper
    {
        public static NoteDto ToDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Priority = note.Priority,
                Tag = note.Tag,
                Text = note.Text,
                //null check operator prevents the execution on the right (after the dot), if the left side is null
                UserFullName = $"{note.User?.FirstName} {note.User?.LastName}"
            };
        }
    }
}