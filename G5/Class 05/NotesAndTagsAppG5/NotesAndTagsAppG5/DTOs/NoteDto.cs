using NotesAndTagsAppG5.Models.Enums;
using System.Globalization;

namespace NotesAndTagsAppG5.DTOs
{
    //DTO == Data Transfer Object
    public class NoteDto
    {
        public string Text { get; set; }
        public string User { get; set; }
        public Priority Priority { get; set; }
        public List<string> Tags { get; set; }
    }
}
