using Azure;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppFluentApi.Domain.Enums;
using SEDC.NotesAppFluentApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppFluentApi.DataAccess
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

            modelBuilder.Entity<Note>()
                .Ignore(x => x.Priority);     
            
            modelBuilder.Entity<Note>()
                .HasOne(note => note.User)
                .WithMany(user => user.Notes)
                .HasForeignKey(note => note.UserId);

            //this does the same job just the starting point is diferent
            //we only need one way relation
            //modelBuilder.Entity<User>()
            //    .HasMany(user => user.Notes)
            //    .WithOne(note => note.User)
            //    .HasForeignKey(note => note.UserId);

            //SEED...
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Dragan",
                    LastName = "Manaskov",
                    Username = "dmanaskov",
                    Age = 27,
                }
            );

            modelBuilder.Entity<Note>().HasData(
                    new Note
                    {
                        Id = 1,
                        Priority = PriorityEnum.High,
                        Tag = TagEnum.Work,
                        Text = "Go to work",
                        UserId = 1
                    }
                );

        }
    }
}
