using SEDC.NotesAppFinal.DTOs.NoteDTOs;

namespace SEDC.NotesAppFinal.Services.Interfaces
{
    public interface INotesService
    {
        Task<NoteDto> GetNoteAsync(int id);
    }
}
