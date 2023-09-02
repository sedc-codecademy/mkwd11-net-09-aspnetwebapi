using Microsoft.EntityFrameworkCore;
using SEDC.NoteApp.Domain.Models;

namespace SEDC.NoteApp.DataAccess.EntityImplementation
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly NoteAppDbContext _noteAppDbContext;
        public NoteRepository(NoteAppDbContext noteAppDbContext)
        {
            _noteAppDbContext = noteAppDbContext;
        }

        public void Add(Note entity)
        {
            _noteAppDbContext.Notes.Add(entity);
            _noteAppDbContext.SaveChanges();
        }

        public void Delete(Note entity)
        {
            _noteAppDbContext.Notes.Remove(entity);
            _noteAppDbContext.SaveChanges();
        }

        public List<Note> GetAll()
        {
            //return _noteAppDbContext.Notes
            //            .Include(x => x.User)
            //            .ToList();

            // EntityFramework behind the scenes
            // we create the query in C#
            var query = _noteAppDbContext.Notes
                            .Include(x => x.User);
            // useful tool to check how our LINQ query is translated to SQL query
            var translatedQuery = query.ToQueryString(); 
            // when calling ToList() our query is translated into SQL query => executed in the database => retrives data
            var materializedQuery = query.ToList();
            return materializedQuery;
            // EntityFramework Executable methods: ToList(), FirstOrDefault(), Any(), SingleOrDefault(), Count()...
        }

        public Note GetById(int id)
        {
            return _noteAppDbContext.Notes
                        .Include(x => x.User)
                        .SingleOrDefault(note => note.Id == id);
        }

        public void Update(Note entity)
        {
            _noteAppDbContext.Notes.Update(entity);
            _noteAppDbContext.SaveChanges();
        }
    }
}
