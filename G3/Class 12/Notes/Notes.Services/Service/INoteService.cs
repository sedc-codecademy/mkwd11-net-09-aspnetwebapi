using Notes.Services.Models;
using Notes.Services.User;

namespace Notes.Services.Service
{
    public interface INoteService
    {
        NoteModel GetNote(ICurrentUser user, int id);

        IEnumerable<NoteModel> GetNotes(ICurrentUser user, SearchNotesModel searchNotesModel);

        NoteModel Create(ICurrentUser user, CreateNoteModel create);

        NoteModel Update(ICurrentUser user, EditNoteModel create);

        NoteModel Delete(ICurrentUser user, int id);

        TagModel AddTag(int noteId, string tagName);
    
        TagModel UpdateTag(int noteId, int tagId, string tagName);

        TagModel RemoveTag(int noteId, int tagId);
    }
}