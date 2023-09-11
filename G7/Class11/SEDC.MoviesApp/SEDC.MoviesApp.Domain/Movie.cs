using SEDC.MoviesApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.MoviesApp.Domain
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        public string Description { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public GenreEnum Genre { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
