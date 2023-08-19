using Notes.Services.Models;

namespace Notes.Services.Service
{
    public interface INoteService
    {
        NoteModel GetNote(int id);

        IEnumerable<NoteModel> GetNotes(SearchNotesModel searchNotesModel);

        NoteModel Create(CreateNoteModel create);

        NoteModel Update(EditNoteModel create);

        NoteModel Delete(int id);

        TagModel AddTag(int noteId, string tagName);
    
        TagModel UpdateTag(int noteId, int tagId, string tagName);

        TagModel RemoveTag(int noteId, int tagId);
    }
}