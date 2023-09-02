using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Enums;

namespace SEDC.MoviesApp.DataAccess
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Movie>().HasData(
            //    new Movie
            //    {
            //        Id = 1,
            //        Title = "Oppenheimer",
            //        Description = "Documentary",
            //        Genre = GenreEnum.Thriller,
            //        Year = 2023
            //    }
            //    );

            //modelBuilder.Entity<Movie>().HasData(
            //    new Movie
            //    {
            //        Id = 2,
            //        Title = "Barbie",
            //        Description = "Girl",
            //        Genre = GenreEnum.Comedy,
            //        Year = 2023
            //    }
            //    );
            //modelBuilder.Entity<Movie>().HasData(
            //    new Movie
            //    {
            //        Id = 3,
            //        Title = "Top Gun:Maverick",
            //        Description = "Action movie",
            //        Genre = GenreEnum.Action,
            //        Year = 2021
            //    }
            //    );
            //modelBuilder.Entity<Movie>().HasData(
            //    new Movie
            //    {
            //        Id = 4,
            //        Title = "Dumb and dumber",
            //        Description = "Action movie",
            //        Genre = GenreEnum.Comedy,
            //        Year = 1994
            //    }
            //    );

            modelBuilder.Entity<Movie>().HasData(new List<Movie>
            {
                new Movie
            {
                Id = 1,
                Title = "Oppenheimer",
                Description = "Documentary",
                Genre = GenreEnum.Thriller,
                Year = 2023
            },
            new Movie
            {
                Id = 2,
                Title = "Barbie",
                Description = "Girl",
                Genre = GenreEnum.Comedy,
                Year = 2023
            },
             new Movie
            {
                Id = 3,
                Title = "Top Gun:Maverick",
                Description = "Action movie",
                Genre = GenreEnum.Action,
                Year = 2021
            },
               new Movie
            {
                Id = 4,
                Title = "Dumb and dumber",
                Description = "Action movie",
                Genre = GenreEnum.Comedy,
                Year = 1994
            }
            });
        }
    }
}
