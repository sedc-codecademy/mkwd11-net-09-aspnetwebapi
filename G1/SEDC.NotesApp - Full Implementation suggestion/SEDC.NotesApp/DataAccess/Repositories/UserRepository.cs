using DataModels;

namespace DataAccess
{
    public class UserRepository : IRepository<UserDto>
    {
        private readonly NotesAppDbContext _context;
        public UserRepository(NotesAppDbContext context)
        {
            _context = context;
        }

        public void Add(UserDto entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(UserDto entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _context.Users;
        }

        public UserDto GetById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public void Update(UserDto update)
        {
            _context.Users.Update(update);
            _context.SaveChanges();
        }
    }
}
