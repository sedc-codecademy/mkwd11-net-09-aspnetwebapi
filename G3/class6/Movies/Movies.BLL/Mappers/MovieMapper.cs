using Movies.BLL.Dtos;
using Movies.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Mappers
{
    public static class MovieMapper
    {
        public static MovieDto MapToDto(this Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Description = movie.Description,
                Genre = movie.Genre,
                Title = movie.Title,
                Year = movie.Year,
            };
        }
    }
}
