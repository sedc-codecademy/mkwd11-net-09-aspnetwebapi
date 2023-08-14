using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAndTagsApp2.DTOs;
using SEDC.NotesAndTagsApp2.Models;

namespace SEDC.NotesAndTagsApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<NoteDto>> Get()
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
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id can not be negative!");
                }

                //try to find the note by id
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    //404
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }


        [HttpGet("findById")] //http://localhost:[port]/api/notes/findById?id=2
        public ActionResult<NoteDto> FindById(int id) //id is a query string param
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id can not be negative!");
                }

                //try to find the note by id
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    //404
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                //validations
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == updateNoteDto.Id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {updateNoteDto.Id} was not found!");
                }

                if (string.IsNullOrEmpty(updateNoteDto.Text))
                {
                    return BadRequest($"Text is a required field");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == updateNoteDto.UserId);
                if (userDb == null)
                {
                    return NotFound($"User with id {updateNoteDto.UserId} was not found!");
                }

                List<Tag> tags = new List<Tag>();
                foreach (int tagId in updateNoteDto.TagIds)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if (tagDb == null)
                    {
                        return NotFound($"Tag with id {tagId} was not found!");
                    }
                    tags.Add(tagDb);
                }

                //update
                noteDb.Text = updateNoteDto.Text;
                noteDb.Priority = updateNoteDto.Priority;
                noteDb.User = userDb;
                noteDb.UserId = userDb.Id;
                noteDb.Tags = tags;

                //return result
                return StatusCode(StatusCodes.Status204NoContent, "Note updated");
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id has invalid value");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    //404
                    return NotFound($"Note with id {id} was not found!");
                }

                StaticDb.Notes.Remove(noteDb);
                return Ok();
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpGet("user/{userId}")] //route params
        public ActionResult<List<NoteDto>> GetNotesByUser(int userId)
        {
            try
            {
                //if no notes are found for the userId, userNotes will be an empty collection
                var userNotes = StaticDb.Notes.Where(x => x.UserId == userId).ToList();
                var userNotesDto = userNotes.Select(x => new NoteDto
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                //validations
                if (string.IsNullOrEmpty(addNoteDto.Text))
                {
                    return BadRequest($"Text is a required field");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == addNoteDto.UserId);
                if (userDb == null)
                {
                    return NotFound($"User with id {addNoteDto.UserId} was not found!");
                }

                List<Tag> tags = new List<Tag>();
                foreach (int tagId in addNoteDto.TagIds)
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
                    Id = ++StaticDb.Notes.LastOrDefault().Id,
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }
    }
}
