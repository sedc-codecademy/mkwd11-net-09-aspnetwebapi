using NotesAppG5DataAnnotations.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppG5DataAnnotations.Domain.Models
{
    [Table("Notes")] //the corresponding table will be called Notes
    public class Note
    {
        [Key] //Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] //not null
        [MaxLength(100)]
        public string Text { get; set; }

        [Required]
        public PriorityEnum Priority { get; set; }

        [Required]
        public TagEnum Tag { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }  //part 1 from 1-M relationship with Note

        [NotMapped] //states that the property will not be mapped in the table
        public int NoteCount { get; set; }
    }
}
