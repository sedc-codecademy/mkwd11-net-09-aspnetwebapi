using SEDC.MoviesApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace SEDC.MoviesApp.Dtos
{
    public class UpdateMovieDtoDataAnnotaions
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(250, ErrorMessage = "Description must be less than 251 characters")]
        public string Description { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public GenreEnum Genre { get; set; }
    }
}
