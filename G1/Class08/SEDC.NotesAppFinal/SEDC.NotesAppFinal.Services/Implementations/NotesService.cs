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

        public async Task CreateNoteAsync(CreateNoteDto note)
        {
            Note noteEntity = note.MapToNote();

            await _noteRepository.CreateAsync(noteEntity);
        }

        public async Task<List<NoteDto>> GetAllNotesAsync()
        {
            List<Note> notes = await _noteRepository.GetAllAsync();

            if (notes == null)
            {
                throw new Exception("Notes are null");
            }

            return notes.Select(note => note.MapToNoteDto()).ToList();
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
