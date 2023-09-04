using MoviesApp_Part2.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp_Part2.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAll();

        MovieDto GetById(int id);

        List<MovieDto> Filter(int? year, int? genre);
    }
}
