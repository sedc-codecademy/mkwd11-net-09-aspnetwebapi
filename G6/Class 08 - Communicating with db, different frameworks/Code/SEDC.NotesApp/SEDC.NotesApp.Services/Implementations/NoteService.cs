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
        private readonly IRepository<User> _userRepository;

        public NoteService(IRepository<Note> noteRepository, IRepository<User> userRepository) //Dependency Injection
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
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
                throw new ResourceNotFoundException($"Note with id {id} was not found!");
            }
            NoteDto noteDto = noteDb.ToNoteDto();
            return noteDto;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            //1. validation
            User userDb = ValidateNoteData(addNoteDto);

            //2. map to domain model
            Note newNote = addNoteDto.ToNote();
            newNote.User = userDb;

            //3. add to db
            _noteRepository.Add(newNote);
        }

        public void UpdateNote(UpdateNoteDto updateNoteDto)
        {
            //1. validate that the note for update exists
            Note noteDb = _noteRepository.GetById(updateNoteDto.Id);
            if(noteDb == null)
            {
                throw new ResourceNotFoundException($"Note with id {updateNoteDto.Id} was not found");
            }

            //2. validate incoming data
            User userDb = ValidateNoteData(updateNoteDto);

            //3. edit data
            //we always update the record that is already from db
            noteDb.Text = updateNoteDto.Text;
            noteDb.Priority = updateNoteDto.Priority;
            noteDb.Tag = updateNoteDto.Tag;
            noteDb.UserId = updateNoteDto.UserId;
            noteDb.User = userDb;

            //4. update in db
            _noteRepository.Update(noteDb);
        }

        private User ValidateNoteData(AddNoteDto addNoteDto)
        {
            User userDb = _userRepository.GetById(addNoteDto.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {addNoteDto.UserId} does not exist");
            }

            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text is required field");
            }
            //Text is not null or empty
            if (addNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text can not contain more than 100 characters");
            }

            return userDb;
        }

        public void DeleteNote(int id)
        {
            //1. validate and get the note
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new ResourceNotFoundException($"Note with id {id} was not found");
            }

            //2. delete the note
            _noteRepository.Delete(noteDb);
        }
    }
}
