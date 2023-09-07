using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Shared;
using Serilog;
using System.Security.Claims;
using System.Text.Json.Serialization;

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

        //[Authorize] //user must be logged in to access this method (must send a token)
        [HttpGet]
        public ActionResult<List<NoteDto>> GetAll()
        {
            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var claims = identity.Claims;

                //if(identity.FindFirst("userRole").Value != "Admin")
                //{
                //    return StatusCode(StatusCodes.Status403Forbidden);
                //}

                throw new Exception("Our error");

                return Ok(_noteService.GetAll());
            }
            catch(Exception e)
            {
                Log.Error("An error ocurred");
                string exeptionString = JsonConvert.SerializeObject(e);
                Log.Error(exeptionString);

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

        //note can be added only from user with role admin or superAdmin
        [Authorize] //we must know the user
        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                //we must validate if the user is with role admin or superAdmin
                string role = User.Claims.First(x => x.Type == "userRole").Value;
                if(role != "admin" && role != "superAdmin")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                Log.Information("User authorized!");

                _noteService.AddNote(addNoteDto);

                Log.Information("Note added");
                return StatusCode(StatusCodes.Status201Created, "Note added");
            }
            catch (DataException e)
            {
                Log.Error("Data related error occurred");
                string exeptionString = JsonConvert.SerializeObject(e);
                Log.Error(exeptionString);

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
