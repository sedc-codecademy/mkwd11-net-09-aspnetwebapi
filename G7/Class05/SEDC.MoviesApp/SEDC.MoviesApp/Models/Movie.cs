using SEDC.MoviesApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SEDC.MoviesApp.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        public string Description { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public GenreEnum Genre { get; set; }

    }
}
