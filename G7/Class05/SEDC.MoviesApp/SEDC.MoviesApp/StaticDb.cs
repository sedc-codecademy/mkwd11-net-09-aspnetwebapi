using SEDC.MoviesApp.Models;
using SEDC.MoviesApp.Models.Enums;

namespace SEDC.MoviesApp
{
    public static class StaticDb
    {
        public static int MovieId = 4;
        public static List<Movie> Movies = new List<Movie>()
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
        };
    }
}
