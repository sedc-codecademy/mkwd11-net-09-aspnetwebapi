using Microsoft.EntityFrameworkCore;
using Notes.Data.Domain;

namespace Notes.Data.Data
{
    public class NotesDbContext
        : DbContext
    {
        public NotesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Tag>()
                .Property(x => x.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<Note>()
                .Property(x => x.Title)
                .HasMaxLength(100);
            
        }
    }
}
