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
        private IRepository<User> _userRepository;

        public NoteService(IRepository<Note> notesRepository, IRepository<User> userRepository)
        {
            _notesRepository = notesRepository;
            _userRepository = userRepository;
        }

        public List<NoteDto> GetAll()
        {
            var notes = _notesRepository.GetAll();

            return notes.Select(x => x.ToDto()).ToList();
        }

        public void AddNote(AddNoteDto note)
        {
            //1. validation
            var user = _userRepository.GetById(note.UserId);
            if(user == null)
            {
                throw new KeyNotFoundException($"User with id {note.UserId} is not found");
            }

            if(string.IsNullOrEmpty(note.Text))
            {
                throw new KeyNotFoundException("Note text is required");
            }

            if(note.Text.Length > 100)
            {
                throw new InvalidDataException("Max length for text is 100 chars");
            }

            //2. map from dto to domain
            //var noteDomain = new Note
            //{
            //    Priority = note.Priority,
            //    Text = note.Text,
            //    Tag = note.Tag,
            //    UserId = note.UserId
            //};

            //var noteDomain = NoteMapper.ToDomain(note);
            var noteDomain = note.ToDomain();

            //3. save the domain model in database
            _notesRepository.Add(noteDomain);
        }
    }
}
