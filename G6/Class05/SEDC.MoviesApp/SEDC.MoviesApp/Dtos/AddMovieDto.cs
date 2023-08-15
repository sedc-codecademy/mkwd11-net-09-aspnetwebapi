using SEDC.MoviesApp.Models;
using SEDC.MoviesApp.Models.Enums;

namespace SEDC.MoviesApp.Dtos
{
    public class AddMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public GenreEnum Genre { get; set; }
    }
}
