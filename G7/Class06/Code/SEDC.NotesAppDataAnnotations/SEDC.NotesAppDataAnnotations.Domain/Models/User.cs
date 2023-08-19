using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppDataAnnotations.Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        //In this case the propery is NOT NULL
        public string FirstName { get; set; }
        //In this case the propery can be nullable
        public string? LastName { get; set; }
        [Required]
        [MaxLength(50)]
        //In this case the propery is NOT NULL
        public string? Username { get; set; }
        public int Age { get; set; }
        [InverseProperty("User")]
        public virtual List<Note> Notes { get; set; }
    }
}
