using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Domain.Domain;

namespace SEDC.MoviesApp.Dtos
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public GenreEnum Genre { get; set; }
    }
}
