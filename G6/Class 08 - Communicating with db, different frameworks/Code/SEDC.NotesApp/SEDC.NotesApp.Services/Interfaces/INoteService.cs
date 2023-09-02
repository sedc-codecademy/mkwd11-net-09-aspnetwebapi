using SEDC.NotesApp.Dtos.Notes;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAll();
        NoteDto GetById(int id);
        void AddNote(AddNoteDto addNoteDto);
        void UpdateNote(UpdateNoteDto updateNoteDto);
        void DeleteNote(int id);
    }
}
