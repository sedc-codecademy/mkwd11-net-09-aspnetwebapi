using AutoMapper;
using Notes.Data.Domain;
using Notes.Data.Repositories;
using Notes.Services.Exceptions;
using Notes.Services.Models;
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

        public NoteService(INotesRepository notesRepository, IMapper mapper)
        {
            this.notesRepository = notesRepository;
            this.mapper = mapper;
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

        public NoteModel Create(CreateNoteModel create)
        {
            var note = new Note
            {
                Description = create.Description,
                Title = create.Title
            };
            notesRepository.Create(note);
            return mapper.Map<NoteModel>(note);
        }

        public NoteModel Delete(int id)
        {
            var entity = notesRepository.GetById(id);
            if(entity == null)
            {
                throw new NotFoundException();
            }
            notesRepository.Remove(entity);
            return mapper.Map<NoteModel>(entity);
        }

        public NoteModel GetNote(int id)
        {
            Data.Domain.Note? note = notesRepository.GetById(id);
            if (note == null)
            {
                throw new NotFoundException($"Note with id {id} can't be found"); // 404
            }

            return mapper.Map<NoteModel>(note);
        }

        public IEnumerable<NoteModel> GetNotes(SearchNotesModel searchNotesModel)
        {
            IEnumerable<Data.Domain.Note> notes = notesRepository.GetNotes(searchNotesModel.Title, searchNotesModel.Description);
            return notes.Select(x => mapper.Map<NoteModel>(x));
        }

        public NoteModel Update(EditNoteModel edit)
        {
            var entity = notesRepository.GetById(edit.Id);
            if(entity == null)
            {
                throw new NotFoundException();
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
