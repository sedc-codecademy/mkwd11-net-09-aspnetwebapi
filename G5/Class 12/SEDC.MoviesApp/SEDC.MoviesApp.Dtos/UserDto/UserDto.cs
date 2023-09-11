using SEDC.MoviesApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.Dtos.UserDto
{
    public class UserDto
    {
        public UserDto()
        {
            MovieList = new List<MovieDto>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public GenreEnum FavoriteGenre { get; set; }
        public List<MovieDto> MovieList { get; set; }
    }
}
