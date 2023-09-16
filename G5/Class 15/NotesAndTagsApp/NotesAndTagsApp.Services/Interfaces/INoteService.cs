using NotesAndTagsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAllNotes();
        List<NoteDto> GetAllUserNotes(int userId);

        NoteDto GetById(int id);

        void AddNote(AddNoteDto addNoteDto);

        void UpdateNote(UpdateNoteDto updateNoteDto);

        void DeleteNote(int id);
    }
}
