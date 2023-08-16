using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Domain.Models
{
    public class User
    {
        [Key] //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Username { get; set; } = string.Empty;

        public int Age { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}