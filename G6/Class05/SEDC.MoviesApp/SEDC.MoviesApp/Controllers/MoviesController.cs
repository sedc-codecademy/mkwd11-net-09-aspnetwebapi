using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MoviesApp.Dtos;
using SEDC.MoviesApp.Models;
using SEDC.MoviesApp.Models.Enums;

namespace SEDC.MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet] //api/movies
        public ActionResult<List<MovieDto>> Get()
        {
            try
            {
                var moviesDb = StaticDb.Movies;
                var movies = moviesDb.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }).ToList();

                return Ok(movies);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("{id}")] //api/movies/2
        public ActionResult<MovieDto> GetId(int id)
        {
            try
            {
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound($"Movie with id {id} was not found");
                }
                var movieDto =  new MovieDto
                {
                    Description = movieDb.Description,
                    Genre = movieDb.Genre,
                    Title = movieDb.Title,
                    Year = movieDb.Year
                };

                return Ok(movieDto);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("queryString")] //api/movies/queryString?index=1
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound($"Movie with id {id} was not found");
                }
                var movieDto = new MovieDto
                {
                    Description = movieDb.Description,
                    Genre = movieDb.Genre,
                    Title = movieDb.Title,
                    Year = movieDb.Year
                };
                return Ok(movieDto);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("addMovie")]
        public IActionResult AddMovie([FromBody] AddMovieDto movieDto)
        {
            try
            {
                if (string.IsNullOrEmpty(movieDto.Title))
                {
                    return BadRequest("Title must not be empty");
                }
                if (!string.IsNullOrEmpty(movieDto.Description) && movieDto.Description.Length > 250)
                {
                    return BadRequest("Description can not be longer than 250 characters");
                }
                if (movieDto.Year <= 0)
                {
                    return BadRequest("Year can not have negative value");
                }
                Movie movie = new Movie()
                {
                    Id = ++StaticDb.MovieId,
                    Year = movieDto.Year,
                    Title = movieDto.Title,
                    Genre = movieDto.Genre,
                    Description = movieDto.Description

                };
                StaticDb.Movies.Add(movie);
                return StatusCode(StatusCodes.Status201Created,"Movie created");
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPut]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto movie)
        {
            try
            {
                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == movie.Id);
                if (movieDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Resource not found");
                if (string.IsNullOrEmpty(movie.Title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Text must not be empty");
                }
                if (movie.Year <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Year must not be negative");
                }
                if (!string.IsNullOrEmpty(movie.Description) && movie.Description.Length > 250)
                {
                    return BadRequest("Description can not be longer than 250 characters");
                }

                movieDb.Year = movie.Year;
                movieDb.Title = movie.Title;
                movieDb.Description = movie.Description;
                movieDb.Genre = movie.Genre;
                return StatusCode(StatusCodes.Status204NoContent, "Movie updated!");
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpDelete]
        public IActionResult DeleteMovie([FromBody] int id)
        {
            try
            {
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }
                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Deleted resource");
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }
                StaticDb.Movies.Remove(movieDb);

                return StatusCode(StatusCodes.Status204NoContent, "Deleted resource");
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("filter")]   //api/movies/filter?genre=1&year=2022  
        public ActionResult<List<MovieDto>> FilterNotesFromQuery(int? genre, int? year)
        {
            try
            {
                if (genre == null && year == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }

                if (year == null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                    var moviesByGender = moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    });
                    return Ok(moviesByGender);
                }
                if (genre == null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Year == year).ToList();

                    var movieByYear = moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    });
                    return Ok(movieByYear);
                }
                List<Movie> movies = StaticDb.Movies.Where(x => x.Year == year
                                                             && x.Genre == (GenreEnum)genre).ToList();

                var movieDto = movies.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                });
                return Ok(movieDto);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
