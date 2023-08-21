using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppDataAnnotations.DataAccess;
using SEDC.NotesAppDataAnnotations.Domain.Models;

namespace SEDC.NotesAppDataAnnotations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NotesAppDbContext _context;
        public UsersController(NotesAppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        ///     Get all Users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                List<User> notes = _context.Users.ToList();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///     Get all Users
        /// </summary>
        /// <returns>List of Users where the Notes are included</returns>
        [HttpGet("include")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllInclude()
        {
            try
            {
                List<User> users = _context.Users.Include(x => x.Notes).ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }      
        }
    }
}
