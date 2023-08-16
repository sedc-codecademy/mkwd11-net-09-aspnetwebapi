using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Dtos;
using Movies.BLL.Services;
using Movies.DAL.Entities;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet("{id}")]//api/movies/1
        public IActionResult GetMovie(int id)
        {
            return Ok(moviesService.GetMovie(id));
        }

        [HttpGet("from-query")]// api/movies?id=1
        public IActionResult GetMovieFromQuery([FromQuery] int id)
        {
            return Ok(moviesService.GetMovie(id));
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(moviesService.GetMovies());
        }

        [HttpGet("filtered")]
        public IActionResult GetMovies(Genre? genre, int? year)
        {
            return Ok(moviesService.GetFilteredMovies(genre, year));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateMovieDto create)
        {
            return Ok(moviesService.Create(create));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] MovieDto movie, int id)
        {
            movie.Id = id;
            return Ok(moviesService.Update(movie));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return Ok(moviesService.Remove(id));
        }

        [HttpPost("delete")]
        public IActionResult RemovePost([FromBody] int id)
        {
            return Ok(moviesService.Remove(id));
        }
    }
}
