using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Models;

namespace SEDC.NoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        
        [HttpGet] //http://localhost:[port]/api/Notes
        public ActionResult<List<NoteDto>> GetNotes()
        {
            try
            {
                var notesDb = StaticDb.Notes;
                var notes = notesDb.Select(x => new NoteDto
                {
                    Priority = x.Priority,
                    Text = x.Text,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags.Select(t => t.Name).ToList()

                }).ToList();
                return Ok(notes);
            }
            catch {

                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }
        }

        //http://localhost:[port]/api/Notes/2
        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("The id can not be negative!");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if(noteDb == null)
                {
                    return NotFound($"Note with id {id} does not exist!");
                }

                var noteDto = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(t => t.Name).ToList()
                };
                return Ok(noteDto);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }
        }

        //http://localhost:[port]/api/Notes/findById?id=1
        [HttpGet("findById")]
        public ActionResult<NoteDto> FindById(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id can not be negative!");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} does not exist!");
                }

                var noteDto = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(t => t.Name).ToList()
                };
                return Ok(noteDto);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<NoteDto>> FindNoteByUser(int userId)
        {
            try
            {
                var userNotes = StaticDb.Notes.Where(x=>x.UserId == userId).ToList();
                var userNotesDto = userNotes.Select(x=> new NoteDto
                {
                    Priority = x.Priority,
                    Text = x.Text,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags.Select(t => t.Name).ToList()
                }).ToList();

                return Ok(userNotesDto);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("Id has invalid value");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if(noteDb == null)
                {
                    return NotFound($"Note with id {id} was not found!");
                }

                StaticDb.Notes.Remove(noteDb);
                return StatusCode(StatusCodes.Status200OK, "Note is delete");
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }
        }

        [HttpPut]
        public IActionResult UpadateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == updateNoteDto.Id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {updateNoteDto.Id} was not found!");
                }

                if (string.IsNullOrEmpty(updateNoteDto.Text))
                {
                    return BadRequest("Text is a required field");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == updateNoteDto.UserId);
                if (userDb == null)
                {
                    return NotFound($"User with id {updateNoteDto.UserId} was not found!");
                }

                List<Tag> tags = new List<Tag>();
                foreach (int tagsId in updateNoteDto.TagsId)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagsId);
                    if (tagDb == null)
                    {
                        return NotFound($"Tag with id {tagsId} was not found!");
                    }
                    tags.Add(tagDb);
                }

                //update
                noteDb.Text = updateNoteDto.Text;
                noteDb.Priority = updateNoteDto.Priority;
                noteDb.Tags = tags;
                noteDb.UserId = userDb.Id;
                noteDb.User = userDb;

                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }

        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                if (string.IsNullOrEmpty(addNoteDto.Text))
                {
                    return BadRequest("Text is a required field");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == addNoteDto.UserId);
                if (userDb == null)
                {
                    return NotFound($"User with id {addNoteDto.UserId} was not found!");
                }

                List<Tag> tags = new List<Tag>();
                foreach (int tagId in addNoteDto.TagsIds)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if (tagDb == null)
                    {
                        return NotFound($"Tag with id {tagId} was not found!");
                    }
                    tags.Add(tagDb);
                }

                //create
                Note newNote = new Note
                {
                    Id = ++StaticDb.NoteId,
                    Text = addNoteDto.Text,
                    Priority = addNoteDto.Priority,
                    User = userDb,
                    UserId = addNoteDto.UserId,
                    Tags = tags
                };

                StaticDb.Notes.Add(newNote);
                return StatusCode(StatusCodes.Status201Created, "Note created!");
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin");
            }
        }
    }
}
