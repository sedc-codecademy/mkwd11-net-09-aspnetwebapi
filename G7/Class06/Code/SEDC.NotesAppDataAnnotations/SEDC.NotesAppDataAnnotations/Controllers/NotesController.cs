using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppDataAnnotations.DataAccess;
using SEDC.NotesAppDataAnnotations.Domain.Models;

namespace SEDC.NotesAppDataAnnotations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesAppDbContext _context;
        public NotesController( NotesAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Get all Notes
        /// </summary>
        /// <returns>List of Notes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Note>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                List<Note> notes = _context.Notes.ToList();

                return Ok(notes);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Get all Notes
        /// </summary>
        /// <returns>List of Notes where the User is included</returns>
        [HttpGet("include")]
        [ProducesResponseType(typeof(List<Note>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllInclude()
        {      
            try
            {
                List<Note> notes = _context.Notes.Include(x => x.User).ToList();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
