using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppG5DataAnnotations.Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Firstname { get; set; }

        [MaxLength(50)]
        public string? Lastname { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }


        [InverseProperty("User")] //it states the other end (the 1 part) from 1-M relationship
        public List<Note> Notes { get; set; } //M part from 1-M relationship with user
    }
}
