using SEDC.MoviesApp.Domain;

namespace SEDC.MoviesApp.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private MovieAppDbContext _dbContext;

        public Repository(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Add<T>(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            //var dbEntity = GetById(entity.Id);
            //DeleteById(entity.Id);

            _dbContext.Remove<T>(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var dbEntity = GetById(id);

            _dbContext.Remove<T>(dbEntity);
            _dbContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            //_dbContext.Movies.ToList();

            //_dbContext.Set<Movie>(); = _dbContext.Movies;

            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            //_dbContext.Movies.FirstOrDefault(x => x.Id == id);

            var entity = _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} does not exist");

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Update<T>(entity);
            _dbContext.SaveChanges();
        }
    }
}
