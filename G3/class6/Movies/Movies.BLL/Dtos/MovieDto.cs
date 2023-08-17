using Movies.DAL.Entities;

namespace Movies.BLL.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int Year { get; set; }

        public Genre Genre { get; set; }
    }
}