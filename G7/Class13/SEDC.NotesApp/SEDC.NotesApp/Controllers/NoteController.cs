using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos;
using SEDC.NotesApp.Services.Interfaces;
using Serilog;
using System.Security.Claims;

namespace SEDC.NotesApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
               

                var roles = User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();

                var notes = _noteService.GetAll();

                Log.Information("Get all notes");
                //Log.Information("All notes: {notes}", notes);
                //We use the @ symobl do deconstruct objects so hey can be logged corectly
                Log.Information("All notes: {@notes}", notes);

                return Ok(notes);
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend");
            }
        }
        //[Authorize(Roles = $"Admin, Member")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var note = _noteService.GetById(id);
                Log.Information("Note with id {id} was found {@note}", id, note);

                return Ok(note);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error(ex.Message);
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend");
            }
        }

        [HttpGet("filter/{tag}")]
        public IActionResult GetByTag(string tag)
        {
            try
            {
                var note = _noteService.GetByTag(tag);

                return Ok(note);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddNoteDto note)
        {
            try
            {
                _noteService.AddNote(note);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend");
            }
        }
    }
}
