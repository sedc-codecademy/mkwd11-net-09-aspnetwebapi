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

        public async Task DeleteNoteAsync(int id)
        {
            await _noteRepository.DeleteAsync(id);
        }

        public async Task EditNoteAsync(CreateNoteDto createNoteDto, int id)
        {
            Note noteDb = await _noteRepository.GetByIdAsync(id);

            if(noteDb == null)
            {
                throw new Exception("Note not found");
            }

            noteDb.Text = createNoteDto.Text;
            noteDb.Priority = createNoteDto.Priority;
            noteDb.Tag = createNoteDto.Tag;
            noteDb.UserId = createNoteDto.UserId;

            await _noteRepository.UpdateAsync(noteDb);
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
