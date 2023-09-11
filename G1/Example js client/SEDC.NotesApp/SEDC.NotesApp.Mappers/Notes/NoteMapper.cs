using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Notes;

namespace SEDC.NotesApp.Mappers.Notes
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto()
            {
                Color = note.Color,
                Text = note.Text,
                Tag = note.Tag,
                //UserFullname = $"{note.User.FirstName} {note.User.LastName}"
            };
        }

        public static Note ToNote(this AddUpdateNoteDto addNoteDto)
        {
            return new Note
            {
                Color = addNoteDto.Color,
                Text = addNoteDto.Text,
                Tag = addNoteDto.Tag,
                UserId = addNoteDto.UserId
            };
        }
    }
}
