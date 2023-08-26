using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MoviesApp.DTOs;
using SEDC.MoviesApp.Models;
using SEDC.MoviesApp.Models.Enums;

namespace SEDC.MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<MovieDto>> Get()
        {
            try
            {
                var moviesDb = StaticDb.Movies;

                if(moviesDb.Count == 0)
                {
                    return NotFound("No movies available");
                }

                var movies = moviesDb.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                });

                return Ok(movies);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }
        }

        [HttpGet("{id}")] 
        // api/Movies/1
        public IActionResult GetById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The id must be greater than 0");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);

                if(movieDb == null)
                {
                    return NotFound($"Movie with id {id} does not exist!");
                }

                var movie = new MovieDto
                {
                    Title = movieDb.Title,
                    Description = movieDb.Description,
                    Year = movieDb.Year,
                    Genre = movieDb.Genre,
                };

                return Ok(movie);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }
        }


        [HttpGet("index")]
        // api/Movies/index?id=2
        public IActionResult GetByIdWithParams(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The id must be greater than 0");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);

                if (movieDb == null)
                {
                    return NotFound($"Movie with id {id} does not exist!");
                }

                var movie = new MovieDto
                {
                    Title = movieDb.Title,
                    Description = movieDb.Description,
                    Year = movieDb.Year,
                    Genre = movieDb.Genre,
                };

                return Ok(movie);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }
        }
        [HttpGet("filter")]
        // api/Movies/filter?genre=1&year=2023
        public IActionResult FilterFromQuery(int? genre, int? year)
        {
            try
            {
                if(genre is null && year is null)
                {
                    return BadRequest("You have to send atleast one filter parameter");
                }

                var query = StaticDb.Movies;

                if(genre.HasValue)
                {
                    if(!Enum.IsDefined(typeof(GenreEnum), genre))
                    {
                        return BadRequest("Invalid genre value");
                    }

                    query = query.Where(x => x.Genre == (GenreEnum)genre).ToList();
                    // query = query.Where(x => (int)x.Genre == genre).ToList();
                }

                if(year.HasValue)
                {
                    if(DateTime.Now.Year < year || year < 1887)
                    {
                        return BadRequest($"Please enter a year between 1888-{DateTime.Now.Year}");
                    }

                    query = query.Where(x => x.Year == year).ToList();
                }

                // for each Movie we create a MovieDto using the Select in a new List
                var movies = query.Select(x => new MovieDto
                {
                    Title = x.Title,
                    Genre = x.Genre,
                    Year = x.Year,
                    Description = x.Description,
                });
                
                return Ok(movies);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] MovieDto movieDto)
        {
            try
            {
                if(string.IsNullOrEmpty(movieDto.Title))
                {
                    return BadRequest("Title must not be empty");
                }

                if(movieDto.Description.Length > 250)
                {
                    return BadRequest($"Description can't be longer than 250 characters!. Your descriptions has {movieDto.Description.Length} characters");
                }

                if (!Enum.IsDefined(typeof(GenreEnum), movieDto.Genre))
                {
                    return BadRequest("Invalid genre value");
                }

                if (DateTime.Now.Year < movieDto.Year || movieDto.Year < 1887)
                {
                    return BadRequest($"Please enter a year between 1888-{DateTime.Now.Year}");
                }

                Movie movie = new Movie
                {
                    Id = ++StaticDb.MovieId,
                    Title = movieDto.Title,
                    Year = movieDto.Year,
                    Genre = movieDto.Genre,
                    Description = movieDto.Description,
                };

                StaticDb.Movies.Add(movie);

                return StatusCode(StatusCodes.Status201Created, "Movie added to the database!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }
        }


        [HttpPut("update")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto updateMovieDto)
        {
            try
            {
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == updateMovieDto.Id);
                if(movieDb is null)
                {
                    return NotFound($"Movie with id {updateMovieDto.Id} does not exist!");
                }

                if (string.IsNullOrEmpty(updateMovieDto.Title))
                {
                    return BadRequest("Title must not be empty");
                }

                if (updateMovieDto.Description.Length > 250)
                {
                    return BadRequest($"Description can't be longer than 250 characters!. Your descriptions has {updateMovieDto.Description.Length} characters");
                }

                if (!Enum.IsDefined(typeof(GenreEnum), updateMovieDto.Genre))
                {
                    return BadRequest("Invalid genre value");
                }

                if (DateTime.Now.Year < updateMovieDto.Year || updateMovieDto.Year < 1887)
                {
                    return BadRequest($"Please enter a year between 1888-{DateTime.Now.Year}");
                }

                movieDb.Title = updateMovieDto.Title;
                movieDb.Description = updateMovieDto.Description;
                movieDb.Genre = updateMovieDto.Genre;
                movieDb.Year = updateMovieDto.Year;

                return StatusCode(StatusCodes.Status204NoContent, "Movie updated");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }

        }

        [HttpPut("update/data")]
        public IActionResult UpdateMovieWithDataAnnotations([FromBody] UpdateMovieDtoDataAnnotaions updateMovieDto)
        {
            //this uses data annotations to help with the validation
            try
            {
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == updateMovieDto.Id);
                if (movieDb is null)
                {
                    return NotFound($"Movie with id {updateMovieDto.Id} does not exist!");
                }

                if (!Enum.IsDefined(typeof(GenreEnum), updateMovieDto.Genre))
                {
                    return BadRequest("Invalid genre value");
                }

                if (DateTime.Now.Year < updateMovieDto.Year || updateMovieDto.Year < 1887)
                {
                    return BadRequest($"Please enter a year between 1888-{DateTime.Now.Year}");
                }

                movieDb.Title = updateMovieDto.Title;
                movieDb.Description = updateMovieDto.Description;
                movieDb.Genre = updateMovieDto.Genre;
                movieDb.Year = updateMovieDto.Year;

                return StatusCode(StatusCodes.Status204NoContent, "Movie updated");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }

        }

        [HttpDelete("{id}")]
        // api/Movies/1
        public IActionResult Delete(int id)
        {
            try
            {
                if(id <= 0 )
                {
                    return BadRequest("The id must be greater than 0");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if(movieDb is null)
                {
                    return NotFound($"Movie with id {id} does not exist!");
                }

                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Movie deleted");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }
        }

        [HttpDelete("deleteFromBody")]
        // api/Movies/deleteFromBody
        public IActionResult DeleteFromBody([FromBody]int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("The id must be greater than 0");
                }

                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb is null)
                {
                    return NotFound($"Movie with id {id} does not exist!");
                }

                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Movie deleted");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured!");
            }
        }

    }
}
