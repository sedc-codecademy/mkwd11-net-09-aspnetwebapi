using SEDC.NotesAppFinal.DataAccess.Interfaces;
using SEDC.NotesAppFinal.Domain.Models;
using SEDC.NotesAppFinal.DTOs.NoteDTOs;
using SEDC.NotesAppFinal.Mappers;
using SEDC.NotesAppFinal.Services.Interfaces;

namespace SEDC.NotesAppFinal.Services.Implementations
{
    public class NotesService : INotesService
    {
        private readonly IRepository<Note> _noteRepository;

        public NotesService(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<NoteDto> GetNoteAsync(int id)
        {
            Note noteDb = await _noteRepository.GetByIdAsync(id);

            if (noteDb == null)
            {
                throw new Exception("Note is null");
            }

            return noteDb.MapToNoteDto();
        }
    }
}
