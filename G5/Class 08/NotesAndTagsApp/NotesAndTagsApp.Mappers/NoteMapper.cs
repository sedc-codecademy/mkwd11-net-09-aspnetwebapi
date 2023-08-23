using NotesAndTagsApp.Domain.Models;
using NotesAndTagsApp.DTOs;

namespace NotesAndTagsApp.Mappers
{
    public static class NoteMapper
    {

        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Tag = note.Tag,
                Priority = note.Priority,
                Text = note.Text,
                UserFullName = $"{note.User.Firstname} {note.User.Lastname}",
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