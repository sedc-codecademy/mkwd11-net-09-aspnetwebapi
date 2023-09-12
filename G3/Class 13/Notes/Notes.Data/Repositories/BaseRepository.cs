using Notes.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public abstract class BaseRepository<T>
        : IRepository<T> where T : class
    {
        protected readonly NotesDbContext notesDbContext;

        public BaseRepository(NotesDbContext notesDbContext)
        {
            this.notesDbContext = notesDbContext;
        }
        public void Create(T entity)
        {
            notesDbContext.Add(entity);
            notesDbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return notesDbContext.Set<T>();
        }

        public T? GetById(int id)
        {
            return notesDbContext.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            notesDbContext.Remove(entity);
            notesDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            notesDbContext.Update(entity);
            notesDbContext.SaveChanges();
        }
    }
}
