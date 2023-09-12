using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using Serilog;
using System.Security.Claims;

namespace SEDC.NotesApp.Controllers
{
    [Authorize]
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
               
                //var roles = User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();

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
            catch (NoteNotFoundException ex)
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
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend");
            }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Add(AddNoteDto note)
        {
            try
            {
                _noteService.AddNote(note);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NoteDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend");
            }
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.UpdateNote(updateNoteDto);
                return NoContent(); //204
            }
            catch (NoteNotFoundException e)
            {
                return NotFound(e.Message); //404
            }
            catch (NoteDataException e)
            {
                return BadRequest(e.Message); //400
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
                return Ok($"Note with id {id} successfully deleted!");
            }
            catch (NoteNotFoundException e)
            {
                return NotFound(e.Message); //404
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }
    }
}
