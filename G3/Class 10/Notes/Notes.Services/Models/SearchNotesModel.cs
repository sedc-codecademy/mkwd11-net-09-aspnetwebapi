namespace Notes.Services.Models
{
    public class SearchNotesModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public IEnumerable<int> List { get; set; } = new List<int>();
    }
}
