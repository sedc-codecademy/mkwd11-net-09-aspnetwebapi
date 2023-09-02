using MoviesApp_Part2.DataAccess.Interfaces;
using MoviesApp_Part2.Domain.Models;
using MoviesApp_Part2.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp_Part2.DataAccess.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesAppDbContext _context;
        public MovieRepository(MoviesAppDbContext context)
        {
            _context = context;
        }
        public void Add(Movie entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Movie entity)
        {
            throw new NotImplementedException();
        }

        public List<Movie> FilterMovies(int? year, int? genre)
        {
            if(genre == null && year == null)
            {
                return _context.Movies.ToList();
            }

            if(year == null)
            {
                List<Movie> moviesDbGenre = _context.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                return moviesDbGenre;
            }
            if(genre == null)
            {
                List<Movie> moviesDbYear = _context.Movies.Where(x => x.Year == year).ToList();
                return moviesDbYear;
            }
            List<Movie> moviesDb = _context.Movies.Where(x => x.Year == year && x.Genre == (GenreEnum)genre).ToList();
            return moviesDb;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.FirstOrDefault(x => x.Id == id);

          //  return _context.Movies.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
