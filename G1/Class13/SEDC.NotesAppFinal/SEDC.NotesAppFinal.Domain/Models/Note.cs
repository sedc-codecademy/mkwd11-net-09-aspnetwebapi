namespace SEDC.NotesAppFinal.Domain.Models
{
    using SEDC.NotesAppFinal.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Note : BaseEntity
    {
        public string Text { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public Tag Tag { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
