namespace Books.Api.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public DateTime BirthYear { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}