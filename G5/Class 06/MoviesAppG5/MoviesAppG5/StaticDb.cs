using MoviesAppG5.Models;
using MoviesAppG5.Models.Enum;

namespace MoviesAppG5
{
    public static class StaticDb
    {
        public static List<Movie> Movies = new List<Movie>()
        {

            new Movie
            {
                Id = 1,
                Title = "Bad boys",
                Description = "Comedy movie",
                Genre = GenreEnum.Comedy,
                Year = 2019
            },

            new Movie
            {
                Id = 2,
                Title = "James Bond",
                Description ="007",
                Genre = GenreEnum.Action,
                Year = 2022
            }
        };
    }
}

