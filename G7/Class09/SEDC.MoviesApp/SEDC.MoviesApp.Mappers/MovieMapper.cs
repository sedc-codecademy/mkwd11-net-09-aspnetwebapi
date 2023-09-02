using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.Mappers
{
    public static class MovieMapper
    {
        public static MovieDto ToDto (this Movie movie)
        {
            return new MovieDto
            {
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Year = movie.Year
            };
        }
    }
}
