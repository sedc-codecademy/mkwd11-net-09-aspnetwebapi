using MoviesAppG5.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace MoviesAppG5.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int Year { get; set; }
        [Required]
        public GenreEnum Genre { get; set; }    
    }
}
