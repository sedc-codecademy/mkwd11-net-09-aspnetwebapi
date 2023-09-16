using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Services.Interfaces;
using NotesAndTagsApp.Shared.CustomExceptions;
using Serilog;
using System.Security.Claims;

namespace NotesAndTagsApp.Controllers
{
    [Authorize] //the user has to be logged in (provide valid token) while hitting this action (sending request)
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<List<NoteDto>> GetAll()
        {
            try
            {
                var claims = User.Claims;
                string userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string username = claims.First(x => x.Type == ClaimTypes.Name).Value;
                string userFullName = claims.First(x => x.Type == "userFullName").Value;
                string userRole = claims.First(x => x.Type == ClaimTypes.Role).Value;
                if(username != "SEDC" || userRole != "SuperAdmin")
                {
                    Log.Error("The user is not with username superAdmin");
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                Log.Information("Succesfully retrieved notes informations");
                return Ok(_noteService.GetAllNotes());

            }
            catch(Exception ex) {
                Log.Error($"GetAll: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getAllUserNotes")]
        public ActionResult<List<NoteDto>> GetAllUserNotes()
        {
            try
            {
                var userId = GetAuthorizedUserId();

                Log.Information("Succesfully retrieved all user notes informations");
                return _noteService.GetAllUserNotes(userId);
            }
            catch(Exception ex)
            {
                Log.Error($"GetAllUserNotes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetById(int id)
        {
            try
            {
                var noteDto = _noteService.GetById(id); //potential NoteNotFoundException
                Log.Information($"Succesfully retrieved note with id {id}");
                return Ok(noteDto); //status 200
            }
            catch (NoteNotFoundException ex)
            {
                Log.Error($"GetById NotFound: {ex.Message}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error($"GetById: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addnoteDto)
        {
            try
            {
                _noteService.AddNote(addnoteDto);
                Log.Information($"Succesfully add note");
                return StatusCode(StatusCodes.Status201Created, "Note added");
            }
            catch (NoteDataException ex)
            {
                Log.Error($"AddNote BadRequest: {ex.Message}");

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error($"AddNote: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.UpdateNote(updateNoteDto);
                Log.Information($"Succesfull update note");

                return NoContent(); //204

            }
            catch (NoteNotFoundException ex)
            {
                Log.Error($"UpdateNote NotFound: {ex.Message}");

                return NotFound(ex.Message); //404
            }
            catch (NoteDataException ex)
            {
                Log.Error($"UpdateNote BadRequest: {ex.Message}");

                return BadRequest(ex.Message); //400
            }
            catch (Exception ex)
            {
                Log.Error($"UpdateNote: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
                Log.Information($"Succesfull delete note");
                return Ok($"Note with id {id} was successfully deleted!");

            }
            catch (NoteNotFoundException ex)
            {
                Log.Error($"DeleteNote NotFound: {ex.Message}");

                return NotFound(ex.Message); //404
            }
            catch (Exception ex)
            {
                Log.Error($"DeleteNote: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                Log.Error($"{name} identifier claim does not exist!");
                throw new Exception($"{name} identifier claim does not exist!");
            }
            return userId;
        }
    }
}
