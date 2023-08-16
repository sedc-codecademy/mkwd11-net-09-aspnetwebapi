using NotesAndTagsAppG5.Models.Enums;

namespace NotesAndTagsAppG5.DTOs
{
    public class UpdateNoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
