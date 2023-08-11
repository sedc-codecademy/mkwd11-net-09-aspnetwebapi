namespace Books.Api.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;


        public decimal Price { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
