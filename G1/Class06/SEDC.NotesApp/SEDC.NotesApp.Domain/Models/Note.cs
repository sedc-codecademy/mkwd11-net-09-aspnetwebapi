using SEDC.NotesApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.NotesApp.Domain.Models
{
    public class Note
    {
        [Key] //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public Tag Tag { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
