using Movies.DAL.Context;
using Movies.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Repositories
{
    public class MovieRepository
        : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MoviesDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Movie> GetFiltered(Genre? genre, int? year)
        {
            IQueryable<Movie> query = dbContext.Movies;
            if(genre != null)
            {
                query = query.Where(x => x.Genre.HasFlag(genre.Value));
            }
            if(year != null)
            {
                query = query.Where(x => x.Year == year.Value);
            }

            return query;
        }
    }
}
