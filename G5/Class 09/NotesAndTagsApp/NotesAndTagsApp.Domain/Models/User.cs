using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Firstname { get; set; }  
        public string? Lastname { get; set; }  
        public string Username { get; set; }  
        public List<Note> Notes { get; set; }

      //  [NotMapped]
        public int? Age { get; set; } //NotMapped


    }
}
