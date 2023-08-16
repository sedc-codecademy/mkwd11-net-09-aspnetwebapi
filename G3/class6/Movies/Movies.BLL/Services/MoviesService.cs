using Movies.BLL.Dtos;
using Movies.BLL.Exceptions;
using Movies.BLL.Mappers;
using Movies.DAL.Entities;
using Movies.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMovieRepository repository;

        public MoviesService(IMovieRepository repository)
        {
            this.repository = repository;
        }

        public MovieDto Create(CreateMovieDto dto)
        {
            if(dto.Title.Length > 250)
            {
                throw new ValidationException("Can not create movie with title larger then 250 characters");
            }

            if(dto.Description != null && dto.Description.Length > 250)
            {
                throw new ValidationException("Can not create movie with description larger then 250 characters");
            }

            var movie = new Movie()
            {
                Description = dto.Description,
                Genre = dto.Genre,
                Title = dto.Title,
                Year = dto.Year
            };
            repository.Create(movie);
            return movie.MapToDto();
        }

        public IEnumerable<MovieDto> GetFilteredMovies(Genre? genre, int? year)
        {
            return repository.GetFiltered(genre,year)
                .Select(x => x.MapToDto())
                .ToList();
        }

        public MovieDto GetMovie(int id)
        {
            Movie? movie = repository.GetById(id);

            if (movie == null)
            {
                throw new NotFoundException($"Movie with id {id} doesn't exist");
            }

            return movie.MapToDto();
        }



        public IEnumerable<MovieDto> GetMovies()
        {
            return repository.
                GetAll()
                .Select(x => x.MapToDto())
                .ToList();
        }

        public MovieDto Update(MovieDto dto)
        {
            if (dto.Title.Length > 250)
            {
                throw new ValidationException("Can not create movie with title larger then 250 characters");
            }

            if (dto.Description != null && dto.Description.Length > 250)
            {
                throw new ValidationException("Can not create movie with description larger then 250 characters");
            }
            var movie = repository.GetById(dto.Id);

            if (movie == null)
            {
                throw new NotFoundException($"Movie with id {dto.Id} does not exist");
            }
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Genre = dto.Genre;
            movie.Year = dto.Year;
            repository.Update(movie);
            return movie.MapToDto();
        }

        public MovieDto Remove(int id)
        {
            var movie = repository.GetById(id);

            if(movie == null)
            {
                throw new NotFoundException($"Movie with id {id} doesn't exist");
            }

            repository.Remove(movie);
            return movie.MapToDto();
        }
    }
}
