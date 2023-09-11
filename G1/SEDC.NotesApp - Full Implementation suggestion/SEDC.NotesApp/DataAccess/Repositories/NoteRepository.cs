using DataModels;

namespace DataAccess
{
    public class NoteRepository : IRepository<NoteDto>
    {
        private readonly NotesAppDbContext _context;
        public NoteRepository(NotesAppDbContext context)
        {
            _context = context;
        }

        public void Add(NoteDto entity)
        {
            _context.Notes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(NoteDto entity)
        {
            _context.Notes.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<NoteDto> GetAll()
        {
            return _context.Notes;
        }

        public NoteDto GetById(int id)
        {
            return _context.Notes.SingleOrDefault(x => x.Id == id);
        }

        public void Update(NoteDto update)
        {
            _context.Notes.Update(update);
            _context.SaveChanges();
        }
    }
}
