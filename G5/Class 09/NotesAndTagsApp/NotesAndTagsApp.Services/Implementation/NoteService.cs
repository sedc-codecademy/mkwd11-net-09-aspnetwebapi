using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Identity.Client;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Mappers;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTagsApp.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository; //DI
        private readonly IRepository<User> _userRepository;

        public NoteService(IRepository<Note> noteRepository, IRepository<User> userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            //validations
            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text cannot be empty string");
            }

            if (addNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text cannot contain more that 100 characters");
            }

            User userDb = _userRepository.GetById(addNoteDto.UserId);
            if(userDb == null)
            {
                throw new NoteDataException($"User with id {addNoteDto.UserId} does not exist");
            }

            //map to domain
            Note newNote = addNoteDto.ToNote();  //NoteMapper.ToNote(addNoteDto)
            newNote.User = userDb;

            //add to db
            _noteRepository.Add(newNote);
        }

        public void DeleteNote(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found");
            }

            _noteRepository.Delete(noteDb);
        }

        public List<NoteDto> GetAllNotes()
        {
          var notesDb = _noteRepository.GetAll();
          return notesDb.Select(x => x.ToNoteDto()).ToList(); //add reference to mapper project
        }

        public NoteDto GetById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if(noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found");
            }

            NoteDto noteDto = noteDb.ToNoteDto();
            return noteDto;
        }

        public void UpdateNote(UpdateNoteDto updateNoteDto)
        {
            //validation
            Note noteDb = _noteRepository.GetById(updateNoteDto.Id);    
            if(noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {updateNoteDto.Id} was not found");
            }
            if (string.IsNullOrEmpty(updateNoteDto.Text))
            {
                throw new NoteDataException("Text cannot be empty string");
            }

            if (updateNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text cannot contain more that 100 characters");
            }

            User userDb = _userRepository.GetById(updateNoteDto.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {updateNoteDto.UserId} does not exist");
            }

            //update - we update the properties that we want to update
            noteDb.Text = updateNoteDto.Text;
            noteDb.Priority = updateNoteDto.Priority;
            noteDb.Tag = updateNoteDto.Tag;
            noteDb.UserId = updateNoteDto.UserId;
            noteDb.User = userDb;

            _noteRepository.Update(noteDb);

        }
    }
}
