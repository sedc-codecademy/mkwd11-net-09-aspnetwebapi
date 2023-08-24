using SEDC.NoteApp.CustomExceptions;
using SEDC.NoteApp.DataAccess;
using SEDC.NoteApp.Domain.Models;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Mappers;
using SEDC.NoteApp.Services.Abstraction;

namespace SEDC.NoteApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<User> _userRepository;

        public NoteService(IRepository<Note> noteRepository,
                           IRepository<User> userRepository)
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
            throw new NotImplementedException();
        }

        public List<NoteDto> GetAllNotes()
        {
            throw new NotImplementedException();
        }

        public NoteDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateNote(UpdateNoteDto updateNoteDto)
        {
            throw new NotImplementedException();
        }
    }
}
