using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAppG5.Models;
using MoviesAppG5.Models.DTOs;
using MoviesAppG5.Models.Enum;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace MoviesAppG5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<MovieDto>> GetAll()
        {
            try {
                var moviesDb = StaticDb.Movies;
                return Ok(moviesDb.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year,
                }));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")] //api/Movies/2
        public ActionResult<MovieDto> GetByRouteId (int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Bad request, the id cannot be negative number!");
                }

                var moviesDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if(moviesDb == null)
                {
                    return NotFound($"Movie with id {id} was not found");
                }

                return Ok(new MovieDto
                {
                    Description = moviesDb.Description,
                    Title = moviesDb.Title,
                    Genre = moviesDb.Genre,
                    Year = moviesDb.Year
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getMovieWIthQueryString")] //api/Movies/getMovieWIthQueryString?id=1
        public ActionResult<MovieDto> GetMovieByIdWithQuery(int? id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest("The id is a required parameter!");
                }

                if (id <= 0)
                {
                    return BadRequest("Bad request, the id cannot be negative number!");
                }

                var moviesDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (moviesDb == null)
                {
                    return NotFound($"Movie with id {id} was not found");
                }

                return Ok(new MovieDto
                {
                    Description = moviesDb.Description,
                    Title = moviesDb.Title,
                    Genre = moviesDb.Genre,
                    Year = moviesDb.Year
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filter")]   //api/Movies/filter?genre=2&year=2022
        public ActionResult<List<MovieDto>> FilterMovies(int? genre, int? year)
        {
            try
            {
                if(genre == null && year == null)
                {
                    return BadRequest("You have to send at least one filter parameter");
                }

                if (genre.HasValue) //genre != null
                {
                    var enumValues = Enum.GetValues(typeof(GenreEnum))
                                     .Cast<GenreEnum>() //Comedy = 1, Action = 2
                                     .Select(g => (int)g) //1, 2
                                     .ToList();

                    if (!enumValues.Contains(genre.Value)){

                        return NotFound($"The genre with id {genre.Value} was not found");
                    }
                }

                if(genre == null)
                {
                    var moviesYear = StaticDb.Movies.Where(x => x.Year == year).ToList();

                    return Ok(moviesYear.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }));
                }
                if(year == null)
                {
                    List<Movie> moviesGenre = StaticDb.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                    return Ok(moviesGenre.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }));
                }

                List<Movie> movies = StaticDb.Movies.Where(x => x.Year == year && x.Genre == (GenreEnum)genre).ToList();
                return Ok(movies.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("addMovie")]
        public IActionResult AddMovie([FromBody] AddMovieDto addMovieDto)
        {
            try
            {
                //if(string.IsNullOrEmpty(addMovieDto.Title) || addMovieDto.Year == null || addMovieDto.Genre == null)
                //{
                //    return BadRequest("Enter all required parameters!");
                //}

                if (string.IsNullOrEmpty(addMovieDto.Title))
                {
                    return BadRequest("Title is required");
                } 

                if (!string.IsNullOrEmpty(addMovieDto.Description) && addMovieDto.Description.Length > 250) //null.Length -> ERROR
                {
                    return BadRequest("Description cannot be longer than 250 characters!");
                }

                if (addMovieDto.Year == null || addMovieDto.Year <= 0 || addMovieDto.Year > DateTime.Now.Year)
                {
                    return BadRequest("Invalid value for year"); //int cannot be null, this is always false
                }
                if (addMovieDto.Genre == null)
                {
                    return BadRequest("Genre is required");//int cannot be null, this is always false

                }

                var enumValues = Enum.GetValues(typeof(GenreEnum))
                                    .Cast<GenreEnum>() //Comedy = 1, Action = 2
                                    .Select(g => (int)g) //1, 2
                                    .ToList();

                if (!enumValues.Contains((int)addMovieDto.Genre))
                {

                    return NotFound($"The genre with id {(int)addMovieDto.Genre} was not found");
                }


                Movie movie = new Movie()
                {
                    Id = StaticDb.Movies.Count + 1,
                    Year = addMovieDto.Year,
                    Title = addMovieDto.Title,
                    Genre = addMovieDto.Genre,
                    Description = addMovieDto.Description
                };

                StaticDb.Movies.Add(movie);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
