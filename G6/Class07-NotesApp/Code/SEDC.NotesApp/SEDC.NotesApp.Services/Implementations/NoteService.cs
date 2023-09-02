using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Mappers.Notes;
using SEDC.NotesApp.Shared.Shared;

namespace SEDC.NotesApp.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;

        public NoteService(IRepository<Note> noteRepository) //Dependency Injection
        {
            _noteRepository = noteRepository;
        }

        public List<NoteDto> GetAll()
        {
           //1. get all notes from db
           List<Note> notesDb = _noteRepository.GetAll();

            //2. map to dtos and return to caller
           return notesDb.Select(n => n.ToNoteDto()).ToList();
        }

        public NoteDto GetById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found!");
            }
            NoteDto noteDto = noteDb.ToNoteDto();
            return noteDto;
        }
    }
}
