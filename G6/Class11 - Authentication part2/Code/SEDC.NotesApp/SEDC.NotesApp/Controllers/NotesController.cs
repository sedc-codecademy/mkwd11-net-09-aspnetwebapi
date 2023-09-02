using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Shared;
using System.Security.Claims;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService) //Dependency Injection
        {
            _noteService = noteService;
        }

        [Authorize] //user must be logged in to access this method (must send a token)
        [HttpGet]
        public ActionResult<List<NoteDto>> GetAll()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims;

                if(identity.FindFirst("userRole").Value != "Admin")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                return Ok(_noteService.GetAll());
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetById(int id)
        {
            try
            {
                var noteDto = _noteService.GetById(id); //potential NoteNotFoundException
                return Ok(noteDto); // status code => 200
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message); //status code => 404
                //return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                _noteService.AddNote(addNoteDto);
                return StatusCode(StatusCodes.Status201Created, "Note added");
            }
            catch (DataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.UpdateNote(updateNoteDto);
                return NoContent();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (DataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
                return NoContent();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }
    }
}
