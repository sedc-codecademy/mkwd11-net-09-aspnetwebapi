using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Ignore(x => x.Age);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Notes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Note>()
                .Property(x => x.Priority)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(x => x.Tag)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(x => x.Text)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
