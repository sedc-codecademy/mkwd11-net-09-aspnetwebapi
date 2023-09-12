using System.ComponentModel.DataAnnotations;

namespace Notes.Api.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
