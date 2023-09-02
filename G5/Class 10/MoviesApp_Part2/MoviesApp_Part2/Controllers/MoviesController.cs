using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp_Part2.Domain.Models.Enums;
using MoviesApp_Part2.DTOS;
using MoviesApp_Part2.Services.Interfaces;

namespace MoviesApp_Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<List<MovieDto>> GetAll()
        {
            try
            {
                return Ok(_movieService.GetAll());  
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);    
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetById(int id) {
            try
            {
                return Ok(_movieService.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<MovieDto>> FilterMovies(int? year, int? genre)
        {
            try
            {
                return Ok(_movieService.Filter(year, genre));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
