using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Domain.Enums;
using SEDC.MoviesApp.Enums;
using System.Text;
using XSystem.Security.Cryptography;

namespace SEDC.MoviesApp.DataAccess
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

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

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("pas123"));
            var hashedPAssword = Encoding.ASCII.GetString(md5data);


            modelBuilder.Entity<User>().HasData(
                    new User()
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        UserName = "user1",
                        Password = hashedPAssword,
                        Role = RoleEnum.Admin
                    }
                );


            modelBuilder.Entity<Movie>().HasData(new List<Movie>
            {
                new Movie
            {
                Id = 1,
                Title = "Oppenheimer",
                Description = "Documentary",
                Genre = GenreEnum.Thriller,
                Year = 2023,
                UserId = 1,
            },
            new Movie
            {
                Id = 2,
                Title = "Barbie",
                Description = "Girl",
                Genre = GenreEnum.Comedy,
                Year = 2023,
                UserId = 1,
            },
             new Movie
            {
                Id = 3,
                Title = "Top Gun:Maverick",
                Description = "Action movie",
                Genre = GenreEnum.Action,
                Year = 2021,
                UserId = 1,
            },
               new Movie
            {
                Id = 4,
                Title = "Dumb and dumber",
                Description = "Action movie",
                Genre = GenreEnum.Comedy,
                Year = 1994,
                UserId = 1,
            }
            });
        }
    }
}
