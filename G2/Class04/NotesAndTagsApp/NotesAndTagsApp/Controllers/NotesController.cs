using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Models;

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //https://localhost:7056/api/notes
        public IActionResult Get()
        {
            try
            {
                var notesDb = StaticDb.Notes;

                var notesDTO = notesDb.Select(note => new NoteDto
                {
                    Priority = note.Priority,
                    Text = note.Text,
                    User = $"{note.User.FirstName} {note.User.LastName}",
                    Tags = note.Tags.Select(tag => tag.Name).ToList()
                }).ToList();

                return Ok(notesDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact admin.");
            }
        }

        [HttpGet("{id}")] //https://localhost:7056/api/notes/1
        public IActionResult GetNoteById([FromRoute] int id) 
        {
            try
            {
                if (id <= 0) 
                {
                    return BadRequest("The id can not be negative!");
                }

                var noteDb = StaticDb.Notes.FirstOrDefault(note => note.Id == id);

                if (noteDb is null) 
                {
                    return NotFound($"Note with id: {id} does not exist");
                }

                var noteDTO = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(tag => tag.Name).ToList()
                };

                return Ok(noteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact admin.");
            }
        }

        [HttpGet("GetNoteFromId")] //https://localhost:7056/api/notes/GetNoteFromId?id=1
        public IActionResult GetNoteFromId([FromQuery] int id) 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("The id can not be negative!");
                }

                var noteDb = StaticDb.Notes.FirstOrDefault(note => note.Id == id);

                if (noteDb is null)
                {
                    return NotFound($"Note with id: {id} does not exist");
                }

                var noteDTO = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(tag => tag.Name).ToList()
                };

                return Ok(noteDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact admin.");
            }
        } 



    }
}
