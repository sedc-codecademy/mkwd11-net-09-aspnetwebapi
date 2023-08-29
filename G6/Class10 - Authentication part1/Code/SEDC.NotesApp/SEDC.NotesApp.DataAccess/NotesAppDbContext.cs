using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //NOTE

            modelBuilder.Entity<Note>()
                .Property(x => x.Text) //column
                .HasMaxLength(50) // max length
                .IsRequired(); //not null

            modelBuilder.Entity<Note>()
                .Property(x => x.Tag) //column
                .IsRequired(); //not null

            modelBuilder.Entity<Note>()
                .Property(x => x.Priority) //column
                .IsRequired(); //not null

            //Relation

            modelBuilder.Entity<Note>()
                .HasOne(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId);


            //User
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName) //column
                .HasMaxLength(50); // max length

            modelBuilder.Entity<User>()
                .Property(x => x.LastName) //column
                .HasMaxLength(50); // max length

            modelBuilder.Entity<User>()
                .Property(x => x.Username) //column
                .HasMaxLength(20) // max length
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(x => x.Password) //column
               .IsRequired();

            modelBuilder.Entity<User>()
                .Ignore(x => x.Age);

            //SEED..

            modelBuilder.Entity<User>()
                .HasData(
                    new User { Id = 1, FirstName = "John", LastName = "Doe", Username = "john_doe", Password = "Test" },
                    new User { Id = 2, FirstName = "Jane", LastName = "Smith", Username = "jane_smith", Password = "Test" }
                );

            modelBuilder.Entity<Note>()
                .HasData(
                    new Note { Id = 1, Text = "Buy groceries", Priority = Priority.High, Tag = Tag.SocialLife, UserId = 1 },
                    new Note { Id = 2, Text = "Finish project report", Priority = Priority.Medium, Tag = Tag.Work, UserId = 2 },
                    new Note { Id = 3, Text = "Call friends", Priority = Priority.Low, Tag = Tag.Health, UserId = 1 }
                );

        }
    }
}
