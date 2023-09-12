using SEDC.NotesApp.Dtos.Notes;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAll();
        void AddNote(AddNoteDto note);
        NoteDto GetById(int id);
        NoteDto GetByTag(string tag);
        void UpdateNote(UpdateNoteDto note);
        void DeleteNote(int id);
    }
}
