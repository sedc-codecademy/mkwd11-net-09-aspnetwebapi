using SEDC.NoteApp.CustomExceptions;
using SEDC.NoteApp.DataAccess;
using SEDC.NoteApp.DataAccess.Abstraction;
using SEDC.NoteApp.Domain.Models;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Mappers;
using SEDC.NoteApp.Services.Abstraction;

namespace SEDC.NoteApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IUserRepository _userRepository;

        public NoteService(IRepository<Note> noteRepository,
                           IUserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            //1. validation
            var userDb = _userRepository.GetById(addNoteDto.UserId);
            if (userDb is null)
            {
                throw new NoteDataException($"User with id {addNoteDto.UserId} does not exist");
            }

            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text is required field");
            }

            if (addNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text can not contain more than 100 characters");
            }

            //2. map to domain(database) model
            var newNoteDb = addNoteDto.ToNote();

            //3. adding note to the database
            _noteRepository.Add(newNoteDb);
        }

        public void DeleteNote(int id)
        {
            var noteFromDb = _noteRepository.GetById(id);

            if (noteFromDb == null) 
            {
                throw new NoteDataException($"Note with id {id} does not exist");
            }

            _noteRepository.Delete(noteFromDb);
        }

        public List<NoteDto> GetAllNotes(int userId)
        {
            var notesFromDb = _noteRepository.GetAll();
            return notesFromDb.Where(note => note.UserId == userId)
                              .Select(note => note.ToNoteDto()).ToList();
        }

        public NoteDto GetById(int id)
        {
            var noteFromDb = _noteRepository.GetById(id);

            if (noteFromDb is null) 
            {
                throw new NoteDataException($"Note with id {id} does not exist");
            }

            return noteFromDb.ToNoteDto();
        }

        public void UpdateNote(UpdateNoteDto updateNoteDto)
        {
            var noteFromDb = _noteRepository.GetById(updateNoteDto.Id);
            if (noteFromDb is null) 
            {
                throw new NoteDataException($"Note with id {updateNoteDto.Id} does not exist");
            }

            var userDb = _userRepository.GetById(updateNoteDto.UserId);
            if (userDb is null)
            {
                throw new NoteDataException($"User with id {updateNoteDto.UserId} does not exist");
            }

            if (string.IsNullOrEmpty(updateNoteDto.Text))
            {
                throw new NoteDataException("Text is required field");
            }

            if (updateNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text can not contain more than 100 characters");
            }

            noteFromDb.Text = updateNoteDto.Text;
            noteFromDb.Priority = updateNoteDto.Priority;
            noteFromDb.Tag = updateNoteDto.Tag;
            noteFromDb.UserId = updateNoteDto.UserId;
            noteFromDb.User = userDb;

            _noteRepository.Update(noteFromDb);
        }
    }
}
