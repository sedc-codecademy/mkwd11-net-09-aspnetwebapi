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
        private readonly IRepository<Note> _noteRepository;

        public NoteService(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            //validations
            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text cannot be empty string");
            }
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
    }
}
