using SEDC.NotesApp.Dtos.Notes;
using System.Collections.Generic;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface INotesService
    {
        List<NoteDto> GetAllNotes();
        NoteDto GetNoteById(int id);
        void AddNote(AddUpdateNoteDto addNoteDto);
        void UpdateNote(AddUpdateNoteDto addNoteDto);
        void DeleteNote(int id);
    }
}
