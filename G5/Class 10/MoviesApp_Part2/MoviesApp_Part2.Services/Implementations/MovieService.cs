using Microsoft.EntityFrameworkCore.Migrations;
using MoviesApp_Part2.DataAccess.Interfaces;
using MoviesApp_Part2.Domain.Models;
using MoviesApp_Part2.Domain.Models.Enums;
using MoviesApp_Part2.DTOS;
using MoviesApp_Part2.Mappers;
using MoviesApp_Part2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp_Part2.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository) {
            _movieRepository = movieRepository;
        }

        public List<MovieDto> Filter(int? year, int? genre)
        {
           if(genre.HasValue)
            {
                var enumValues = Enum.GetValues(typeof(GenreEnum))
                                .Cast<GenreEnum>()
                                .ToList();
                
                if (!enumValues.Contains((GenreEnum)genre.Value))
                {
                    throw new Exception("Invalid genre");
                }
            }

            return _movieRepository.FilterMovies(year, genre).Select(x => x.ToMovieDto()).ToList();
        }

        public List<MovieDto> GetAll()
        {
            return _movieRepository.GetAll().Select(x => x.ToMovieDto()).ToList();
        }

        public MovieDto GetById(int id)
        {
           var movieDb = _movieRepository.GetById(id);
           if(movieDb == null)
            {
                throw new Exception($"Movie with id {id} was not found");
            }

            return movieDb.ToMovieDto();
        }
    }
}
