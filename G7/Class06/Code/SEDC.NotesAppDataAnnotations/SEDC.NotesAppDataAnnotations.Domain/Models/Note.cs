using SEDC.NotesAppDataAnnotations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppDataAnnotations.Domain.Models
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Text { get; set; }
        [Required]
        //[Column("Priority2")]
        public PriorityEnum Priority { get; set; }
        public TagEnum Tag { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
