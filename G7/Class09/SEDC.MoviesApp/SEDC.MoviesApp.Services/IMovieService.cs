using SEDC.MoviesApp.Dtos;

namespace SEDC.MoviesApp.Services
{
    public interface IMovieService
    {
        List<MovieDto> GetAll();
    }
}
