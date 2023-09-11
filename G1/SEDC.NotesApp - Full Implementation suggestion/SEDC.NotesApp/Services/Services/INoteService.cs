using InterfaceModels;

namespace Services
{
    public interface INoteService
    {
        IEnumerable<NoteModel> GetUserNotes(int userId);
        NoteModel GetNote(int id, int userId);
        void AddNote(NoteModel model);
        void DeleteItem(int id, int userId);
    }
}
