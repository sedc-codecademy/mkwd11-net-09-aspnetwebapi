using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.DataAccess.Implementation;
using SEDC.NotesApp.Shared.CustomExceptions;
using SEDC.NotesApp.Dtos.Notes;

namespace SEDC.NotesApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private INoteRepository _notesRepository;
        private IUserRepository _userRepository;

        public NoteService(INoteRepository notesRepository, IUserRepository userRepository)
        {
            _notesRepository = notesRepository;
            _userRepository = userRepository;
        }

        public List<NoteDto> GetAll()
        {
            var notes = _notesRepository.GetAll();

            return notes.Select(x => x.ToDto()).ToList();
        }

        public NoteDto GetById(int id)
        {
            var note = _notesRepository.GetById(id);

            if (note == null)
                throw new NoteNotFoundException($"Note with id {id} is not found");

            return note.ToDto();
        }

        public NoteDto GetByTag(string tag)
        {
            var note = _notesRepository.GetByTag(tag);

            if (note == null)
                throw new NoteNotFoundException($"Note with tag {tag} is not found");

            return note.ToDto();
        }

        public void AddNote(AddNoteDto note)
        {
            //1. validation
            var user = _userRepository.GetById(note.UserId);
            if(user == null)
            {
                throw new UserNotFoundException($"User with id {note.UserId} is not found");
            }

            if(string.IsNullOrEmpty(note.Text))
            {
                throw new NoteDataException("Note text is required");
            }

            if(note.Text.Length > 100)
            {
                throw new NoteDataException("Max length for text is 100 chars");
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

        public void DeleteNote(int id)
        {
            Note noteDb = _notesRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found");
            }

            _notesRepository.Delete(noteDb);
        }

        public void UpdateNote(UpdateNoteDto note)
        {
            //1.validation
            Note noteDb = _notesRepository.GetById(note.Id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {note.Id} was not found!");
            }

            User userDb = _userRepository.GetById(note.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {note.UserId} does not exist");
            }

            if (string.IsNullOrEmpty(note.Text))
            {
                throw new NoteDataException("Text is required field");
            }
            //Text is not null or empty
            if (note.Text.Length > 100)
            {
                throw new NoteDataException("Text can not contain more than 100 characters");
            }
            //Must be the creator
            if(noteDb.UserId != note.UserId)
            {
                throw new NoteDataException("Cant edit other persons note");

            }

            //2.update
            //We must update the object that we read from db
            noteDb.Text = note.Text;
            noteDb.Priority = note.Priority;
            noteDb.Tag = note.Tag;
           

            _notesRepository.Update(noteDb);
        }
    }
}
