using SEDC.NotesApp.Dtos;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAll();
        void AddNote(AddNoteDto note);
        NoteDto GetById(int id);
    }
}
