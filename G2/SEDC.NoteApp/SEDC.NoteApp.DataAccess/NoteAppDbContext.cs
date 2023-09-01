using Microsoft.EntityFrameworkCore;
using SEDC.NoteApp.Domain.Enums;
using SEDC.NoteApp.Domain.Models;
using SEDC.NoteApp.CryptoService;

namespace SEDC.NoteApp.DataAccess
{
    // Microsoft.EntityFrameworkCore
    // Microsoft.EntityFrameworkCore.Design
    // Microsoft.EntityFrameworkCore.SqlServer
    // Microsoft.EntityFrameworkCore.Tools
    public class NoteAppDbContext : DbContext
    {
        public NoteAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //NOTE

            //validations
            modelBuilder.Entity<Note>()
                .Property(x => x.Text)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(x => x.Priority)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(x => x.Tag)
                .IsRequired();

            //relations
            modelBuilder.Entity<Note>()
                .HasOne(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId);

            //USER

            //validations
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
               .Property(x => x.Username)
               .HasMaxLength(30)
               .IsRequired();



            //SEED
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    FirstName = "Viktor",
                    LastName = "Jakovlev",
                    Username = "vjakovlev",
                    Age = 34,
                    Password = StringHasher.Hash("viktor123"),
                    Notes = new List<Note>()
                });

            modelBuilder.Entity<Note>()
                .HasData(new Note
                {
                    Id = 1,
                    Text = "note text",
                    Priority = Priority.Low,
                    Tag = Tag.SocialLife,
                    UserId = 1
                });
        }
    }
}
