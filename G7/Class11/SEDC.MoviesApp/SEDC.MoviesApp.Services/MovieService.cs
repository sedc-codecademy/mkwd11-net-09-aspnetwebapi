using SEDC.MoviesApp.DataAccess;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Dtos;
using SEDC.MoviesApp.Enums;
using SEDC.MoviesApp.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.Services
{
    public class MovieService : IMovieService
    {
        private IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public List<MovieDto> GetAll()
        {
            var movies = _movieRepository.GetAll();

            if(movies.Count == 0)
            {
                throw new KeyNotFoundException("No movies available");
            }

            return movies.Select(x => x.ToDto()).ToList();
        }

        public void Update(UpdateMovieDto updateMovieDto)
        {
            var movieDb = _movieRepository.GetById(updateMovieDto.Id);
            if (movieDb is null)
            {
                throw new KeyNotFoundException($"Movie with id {updateMovieDto.Id} does not exist!");
            }

            if (string.IsNullOrEmpty(updateMovieDto.Title))
            {
                throw new ArgumentException("Title must not be empty");
            }

            if (updateMovieDto.Description.Length > 250)
            {
                throw new ArgumentException($"Description can't be longer than 250 characters!. Your descriptions has {updateMovieDto.Description.Length} characters");
            }

            if (!Enum.IsDefined(typeof(GenreEnum), updateMovieDto.Genre))
            {
                throw new ArgumentException("Invalid genre value");
            }

            if (DateTime.Now.Year < updateMovieDto.Year || updateMovieDto.Year < 1887)
            {
                throw new ArgumentException($"Please enter a year between 1888-{DateTime.Now.Year}");
            }

            movieDb.Title = updateMovieDto.Title;
            movieDb.Description = updateMovieDto.Description;
            movieDb.Genre = updateMovieDto.Genre;
            movieDb.Year = updateMovieDto.Year;

            _movieRepository.Update(movieDb);
        }
    }
}
