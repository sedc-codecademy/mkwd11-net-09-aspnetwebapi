using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                //we select which property whe want do add some constraints 
                .Property(x => x.FirstName)
                // we add those constraints here
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Age)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasMaxLength(100);

            modelBuilder.Entity<Note>()
                .HasOne(note => note.User)
                .WithMany(user => user.Notes)
                .HasForeignKey(note => note.UserId);

            modelBuilder.Entity<Note>().Property(x => x.Text).HasMaxLength(100);

            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = 1,
            //        FirstName = "Dragan",
            //        LastName = "Manaskov",
            //        Username = "dmanaskov",
            //        Age = 27,
            //    }
            //);

            //modelBuilder.Entity<Note>().HasData(
            //        new Note
            //        {
            //            Id = 1,
            //            Priority = PriorityEnum.High,
            //            Tag = TagEnum.Work,
            //            Text = "Go to work",
            //            UserId = 1
            //        }
            //    );
        }
    }
}
