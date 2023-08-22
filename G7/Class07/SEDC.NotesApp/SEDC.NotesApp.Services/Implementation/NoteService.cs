using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Mappers;

namespace SEDC.NotesApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private IRepository<Note> _notesRepository;

        public NoteService(IRepository<Note> notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public List<NoteDto> GetAll()
        {
            var notes = _notesRepository.GetAll();

            return notes.Select(x => x.ToDto()).ToList();
        }
    }
}
