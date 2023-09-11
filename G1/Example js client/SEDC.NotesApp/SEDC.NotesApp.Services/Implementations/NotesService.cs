using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Mappers.Notes;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.NotesApp.Services.Implementations
{
    public class NotesService : INotesService
    {
        private IRepository<Note> _notesRepository;
        private IUserRepository _usersRepository;
        public NotesService(IRepository<Note> notesRepository, IUserRepository usersRepository)
        {
            _notesRepository = notesRepository;
            _usersRepository = usersRepository;
        }

        public void AddNote(AddUpdateNoteDto addNoteDto)
        {
            ValidateNoteInput(addNoteDto);

            Note newNote = addNoteDto.ToNote();
            _notesRepository.Insert(newNote);
        }

        public void DeleteNote(int id)
        {
            Note noteDb = _notesRepository.GetById(id);
            if(noteDb == null)
            {
                throw new ResourceNotFoundException( $"Note with {id} was not found!");
            }
            _notesRepository.Delete(noteDb);
        }

        public List<NoteDto> GetAllNotes()
        {
            //read from db
            List<Note> notesDb = _notesRepository.GetAll();
            //map to dtos
            return notesDb.Select(x => x.ToNoteDto()).ToList();
        }

        public NoteDto GetNoteById(int id)
        {
            Note noteDb = _notesRepository.GetById(id);
            if(noteDb == null)
            {
                //log
                throw new ResourceNotFoundException($"Note with id {id} was not found");
            }
            return noteDb.ToNoteDto();
        }

        public void UpdateNote(AddUpdateNoteDto updateNoteDto)
        {
            Note noteDb = _notesRepository.GetById(updateNoteDto.Id);
            if(noteDb == null)
            {
                throw new ResourceNotFoundException($"Note with id {updateNoteDto.Id} was not found");
            }
            ValidateNoteInput(updateNoteDto);
            noteDb.Text = updateNoteDto.Text;
            noteDb.Color = updateNoteDto.Color;
            noteDb.Tag = updateNoteDto.Tag;
            noteDb.UserId = updateNoteDto.UserId;

            _notesRepository.Update(noteDb);

        }


        #region private methods
        private void ValidateNoteInput(AddUpdateNoteDto addNoteDto)
        {
            User userDb = _usersRepository.GetById(addNoteDto.UserId);
            if (userDb == null)
            {
                throw new NoteException("The user does not exist!");
            }
            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteException("The text must not be empty!");
            }
            if (addNoteDto.Text.Length > 100)
            {
                throw new NoteException("The text must not contain more than 100 characters!");
            }
            if (!string.IsNullOrEmpty(addNoteDto.Color) && addNoteDto.Color.Length > 30)
            {
                throw new NoteException("The color must not contain more than 30 characters!");
            }
        }
        #endregion
    }
}
