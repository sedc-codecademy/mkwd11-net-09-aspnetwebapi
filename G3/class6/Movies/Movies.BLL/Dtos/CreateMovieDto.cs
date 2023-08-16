using Movies.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Dtos
{
    public class CreateMovieDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int Year { get; set; }

        public Genre Genre { get; set; }
    }
}
