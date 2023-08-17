using Movies.DAL.Entities;

namespace Movies.DAL.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        public IEnumerable<Movie> GetFiltered(Genre? genre, int? year);
    }
}
