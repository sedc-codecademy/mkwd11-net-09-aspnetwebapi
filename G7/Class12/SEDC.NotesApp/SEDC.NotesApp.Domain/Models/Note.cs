using SEDC.NotesApp.Domain.Enums;

namespace SEDC.NotesApp.Domain.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public TagEnum Tag { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
