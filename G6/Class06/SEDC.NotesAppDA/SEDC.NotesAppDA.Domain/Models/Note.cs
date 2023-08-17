using SEDC.NotesAppDA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppDA.Domain.Models
{
    [Table("Notes")]
    public class Note : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Text { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public Tag Tag { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}
