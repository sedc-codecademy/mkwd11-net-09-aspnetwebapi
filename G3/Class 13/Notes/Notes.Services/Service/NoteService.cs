using AutoMapper;
using Notes.Data.Domain;
using Notes.Data.Repositories;
using Notes.Services.Exceptions;
using Notes.Services.Models;
using Notes.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Service
{
    public class NoteService : INoteService
    {
        private readonly INotesRepository notesRepository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public NoteService(INotesRepository notesRepository, IMapper mapper, IUserRepository userRepository)
        {
            this.notesRepository = notesRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public TagModel AddTag(int noteId, string tagName)
        {
            var note = notesRepository.GetById(noteId) ?? throw new NotFoundException();
            var tag = new Tag
            {
                Name = tagName,
            };
            note.Tags.Add(tag);
            notesRepository.Update(note);
            return mapper.Map<TagModel>(tag);
        }

        public NoteModel Create(ICurrentUser loggedInUser, CreateNoteModel create)
        {
            var user = userRepository.GetById(loggedInUser.Id);
            var note = new Note
            {
                Description = create.Description,
                Title = create.Title,
                User = user
            };
            user.Notes.Add(note);

            notesRepository.Create(note);
            return mapper.Map<NoteModel>(note);
        }

        public NoteModel Delete(ICurrentUser user, int id)
        {
            var entity = notesRepository.GetById(id);
            if(entity == null)
            {
                throw new NotFoundException();
            }
            if(entity.User.Id != user.Id)
            {
                throw new ExecutionNotAllowedException();
            }
            notesRepository.Remove(entity);
            return mapper.Map<NoteModel>(entity);
        }

        public NoteModel GetNote(ICurrentUser user, int id)
        {
            Data.Domain.Note? note = notesRepository.GetById(id);
            if (note == null)
            {
                throw new NotFoundException($"Note with id {id} can't be found"); // 404
            }
            if (note.User.Id != user.Id)
            {
                throw new ExecutionNotAllowedException();
            }

            return mapper.Map<NoteModel>(note);
        }

        public IEnumerable<NoteModel> GetNotes(ICurrentUser user, SearchNotesModel searchNotesModel)
        {
            IEnumerable<Data.Domain.Note> notes = notesRepository.GetNotes(user.Id, searchNotesModel.Title, searchNotesModel.Description);
            return notes.Select(x => mapper.Map<NoteModel>(x));
        }

        public NoteModel Update(ICurrentUser user, EditNoteModel edit)
        {
            var entity = notesRepository.GetById(edit.Id);
            if(entity == null)
            {
                throw new NotFoundException();
            }
            if(entity.User.Id != user.Id)
            {
                throw new ExecutionNotAllowedException();
            }
            entity.Title = edit.Title;
            entity.Description = edit.Description;
            notesRepository.Update(entity);
            return mapper.Map<NoteModel>(entity);
        }

        public TagModel UpdateTag(int noteId, int tagId, string tagName)
        {
            var note = notesRepository.GetById(noteId) ?? throw new NotFoundException();
            var tag = note.Tags.FirstOrDefault(x => x.Id == tagId);
            if (tag == null)
            {
                throw new NotFoundException();
            }
            tag.Name = tagName;
            notesRepository.Update(note);
            return mapper.Map<TagModel>(tag);
        }


        public TagModel RemoveTag(int noteId, int tagId)
        {
            var note = notesRepository.GetById(noteId) ?? throw new NotFoundException();
            var tag = note.Tags.FirstOrDefault(x => x.Id == tagId);
            if (tag == null)
            {
                throw new NotFoundException();
            }
            note.Tags.Remove(tag);
            notesRepository.Update(note);
            return mapper.Map<TagModel>(tag);
        }
    }
}
