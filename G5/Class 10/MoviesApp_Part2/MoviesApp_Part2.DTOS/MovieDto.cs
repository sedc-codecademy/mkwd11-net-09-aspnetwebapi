using MoviesApp_Part2.Domain.Models.Enums;

namespace MoviesApp_Part2.DTOS
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public GenreEnum Genre { get; set; }
    }
}