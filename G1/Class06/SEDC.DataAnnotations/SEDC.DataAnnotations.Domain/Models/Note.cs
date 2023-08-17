using SEDC.DataAnnotations.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.DataAnnotations.Domain.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; } = string.Empty;

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public Tag Tag { get; set; }

        public int UserId { get; set; }

        //[InverseProperty("Note")]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
