using Movies.BLL.Dtos;
using Movies.DAL.Entities;

namespace Movies.BLL.Services
{
    public interface IMoviesService
    {
        MovieDto GetMovie(int id);

        IEnumerable<MovieDto> GetMovies();

        IEnumerable<MovieDto> GetFilteredMovies(Genre? genre, int? year);
    
        MovieDto Create(CreateMovieDto dto);

        MovieDto Update(MovieDto dto);

        MovieDto Remove(int id);
    }
}