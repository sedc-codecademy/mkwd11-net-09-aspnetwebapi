using SEDC.NoteApp.Domain.Models;
using SEDC.NoteApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Mappers
{
    public static class NoteMapper
    {
        public static Note ToNote(this AddNoteDto addNoteDto) 
        {
            return new Note
            {
                Text = addNoteDto.Text,
                Tag = addNoteDto.Tag,
                Priority = addNoteDto.Priority,
                UserId = addNoteDto.UserId
            };
        }

        public static NoteDto ToNoteDto(this Note note) 
        {
            var noteDto = new NoteDto
            {
                Text = note.Text,
                Tag = note.Tag,
                Priority = note.Priority
            };

            if (note.User is not null) 
            {
                noteDto.UserFullName = $"{note.User.FirstName} {note.User.LastName}";
            }

            return noteDto;
        }
    }
}
