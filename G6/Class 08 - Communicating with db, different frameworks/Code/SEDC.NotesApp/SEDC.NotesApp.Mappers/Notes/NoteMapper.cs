using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Notes;

namespace SEDC.NotesApp.Mappers.Notes
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Priority = (int)note.Priority,
                Tag = (int)note.Tag,
                Text = note.Text,
               // UserFullName = $"{note.User.FirstName} {note.User.LastName}"
            };
        }

        public static Note ToNote(this AddNoteDto addNoteDto)
        {
            return new Note()
            {
                Text = addNoteDto.Text,
                Priority = addNoteDto.Priority,
                Tag = addNoteDto.Tag,
                UserId = addNoteDto.UserId, //FK
            };
        }
    }
}
