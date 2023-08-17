using Movies.DAL.Context;
using Movies.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MoviesDbContext dbContext;

        public Repository(MoviesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public T? GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            //dbContext.Update(entity);
            dbContext.SaveChanges();
        }
    }
}
