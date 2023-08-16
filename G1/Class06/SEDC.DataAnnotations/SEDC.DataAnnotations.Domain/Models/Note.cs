using SEDC.DataAnnotations.Domain.Enums;

namespace SEDC.DataAnnotations.Domain.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public Priority Priority { get; set; }

        public Tag Tag { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
