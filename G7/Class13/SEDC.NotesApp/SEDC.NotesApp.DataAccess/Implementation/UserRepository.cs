using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess.Implementation
{
    public class UserRepository : IRepository<User>
    {
        private NotesAppDbContext _dbContext;

        public UserRepository(NotesAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                throw new KeyNotFoundException($"User with id {id} is not found");

            return user;
        }

        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            var user = GetById(entity.Id);

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User GetByTag(string tag)
        {
            throw new NotImplementedException();
        }
    }
}
