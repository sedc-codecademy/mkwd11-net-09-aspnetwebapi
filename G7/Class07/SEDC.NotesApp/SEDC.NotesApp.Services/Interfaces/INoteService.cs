using SEDC.NotesApp.Dtos;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAll();
    }
}
