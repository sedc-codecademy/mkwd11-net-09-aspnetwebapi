using Azure;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppDataAnnotations.Domain.Enums;
using SEDC.NotesAppDataAnnotations.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppDataAnnotations.DataAccess
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
