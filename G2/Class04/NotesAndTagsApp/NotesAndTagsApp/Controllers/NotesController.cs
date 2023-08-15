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

        [HttpGet("findById")] //https://localhost:7056/api/notes/GetNoteFromId?id=1
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

        [HttpGet("user/{userId}")]
        public IActionResult GetNoteByUser([FromRoute] int userId) 
        {

            try
            {
                var userNotes = StaticDb.Notes.Where(note => note.User.Id == userId)
                                          .Select(note => new NoteDto
                                          {
                                              Priority = note.Priority,
                                              Text = note.Text,
                                              User = $"{note.User.FirstName} {note.User.LastName}",
                                              Tags = note.Tags.Select(tag => tag.Name).ToList()
                                          }).ToList();

                return Ok(userNotes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact admin.");
            }
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto) 
        {
            try
            {
                var userDb = StaticDb.Users.FirstOrDefault(user => user.Id == addNoteDto.UserId);
                if (userDb is null)
                {
                    return NotFound($"User with id: {addNoteDto.UserId} was not found!");
                }

                var tags = new List<Tag>();
                foreach (int tagId in addNoteDto.TagIds)
                {
                    var tag = StaticDb.Tags.FirstOrDefault(tag => tag.Id == tagId);
                    if (tag is null)
                    {
                        return NotFound($"Tag with id {tagId} was not found");
                    }
                    tags.Add(tag);
                }

                var noteDb = new Note
                {
                    Id = ++StaticDb.NoteId,
                    Text = addNoteDto.Text,
                    Priority = addNoteDto.Priority,
                    UserId = userDb.Id,
                    User = userDb,
                    Tags = tags
                };

                StaticDb.Notes.Add(noteDb);

                return StatusCode(StatusCodes.Status201Created, "Note created!s");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact admin.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById([FromRoute] int id) 
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
                    return NotFound($"Note with id {id} was not found!");
                }

                StaticDb.Notes.Remove(noteDb);
                return Ok("Note deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact admin.");
            }
        }

    }
}
