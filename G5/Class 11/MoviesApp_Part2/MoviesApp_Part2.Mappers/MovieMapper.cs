using MoviesApp_Part2.Domain.Models;
using MoviesApp_Part2.DTOS;
using System.Runtime.CompilerServices;

namespace MoviesApp_Part2.Mappers
{
    public static class MovieMapper
    {
         public static MovieDto ToMovieDto(this Movie movie)
         {
            return new MovieDto
            {
                Year = movie.Year,
                Description = movie.Description,
                Title = movie.Title,
                Genre = movie.Genre
            };
         }
    }
}