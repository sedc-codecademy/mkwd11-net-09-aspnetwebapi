using Books.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Data
{
    public class BooksDbContext
        : DbContext
    {
        public BooksDbContext(DbContextOptions options) 
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .Property(x => x.Title)
                .HasMaxLength(255);

            modelBuilder.Entity<Book>()
                .Property(x => x.Description)
                .HasMaxLength(1024);

            modelBuilder.Entity<Book>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Author>()
                .Property(x => x.Name)
                .HasMaxLength(255);

            //modelBuilder.Entity<Author>()
            //    .HasMany(x => x.Books)
            //    .WithMany(x => x.Authors);

            //modelBuilder.Entity<Book>()
            //    .HasMany(x => x.Authors)
            //    .WithMany(x => x.Books);
        }

    }
}
