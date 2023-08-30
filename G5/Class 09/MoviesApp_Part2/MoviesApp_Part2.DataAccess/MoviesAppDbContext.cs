using Microsoft.EntityFrameworkCore;
using MoviesApp_Part2.Domain.Models;
using MoviesApp_Part2.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp_Part2.DataAccess
{
    public class MoviesAppDbContext : DbContext
    {
        public MoviesAppDbContext(DbContextOptions options): base(options) { }

        //DbSet

        DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Movie>()
                 .Property(x => x.Description)
                 .HasMaxLength(250);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Genre)
                .IsRequired();

            //SEED
            modelBuilder.Entity<Movie>()
                .HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "James Bond",
                    Description = "007",
                    Genre = GenreEnum.Action,
                    Year = 2020
                });
        }
    }
}
