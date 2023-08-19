using MoviesAppG5.Models.Enum;

namespace MoviesAppG5.Models.DTOs
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }

        public GenreEnum Genre { get; set; }
    }
}
