using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsAppG5.DTOs;
using NotesAndTagsAppG5.Models;

namespace NotesAndTagsAppG5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  //http://loclahost:[port]/api/Notes
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<NoteDto>> GetAll()
        {
            try
            {
                //return Ok(StaticDb.Notes);

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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")] //http://lochost:[port]/api/Notes/1
        public ActionResult<NoteDto> GetNoteById(int id) //id is a path parameter
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("The id cannot be negative!");
                }
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id); //get from db
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("findById")] //http://loclahost:[port]/api/Notes/findById?id=1
        public ActionResult<Note> FindById(int? id) //id is a query parameter
        {
            try
            {
                if(id == null)
                {
                    return BadRequest("Id is a required parameter!");
                }

                if (id < 0)
                {
                    return BadRequest("The id cannot be negative!");
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       // [HttpGet("{text}/priority/{priority}")] //http://loclahost:[port]/api/Notes/Gym/priority/1
        [HttpGet("{priority}/priority/{text}")] //http://loclahost:[port]/api/Notes/1/priority/Gym
        public ActionResult<List<Note>> FilterNotes(string text, int priority)
        {
            try
            {
                if(string.IsNullOrEmpty(text) || priority <= 0)
                {
                    return BadRequest("Filter parameters are required!");
                }

                if(priority > 3)
                {
                    return BadRequest("Invalid value for priroity!");

                }

                //text = Gym
                List<Note> filteredNotes =     //go to the gym             //gym
                    StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())
                         && (int)x.Priority == priority).ToList();
                return Ok(filteredNotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("multipleQuery")] //http://loclahost:[port]/api/Notes/multipleQuery
        public ActionResult<List<Note>> FilterNotesWithQueryParamas(string? text, int? priority)
        {
            try
            {
                if(string.IsNullOrEmpty(text) && priority == null)
                {
                    return BadRequest("You need to send at least one filter parameter!");
                }
                if (string.IsNullOrEmpty(text))
                {
                    List<Note> filteredNotesByPriority = StaticDb.Notes.Where(x => (int)x.Priority == priority).ToList();
                    return Ok(filteredNotesByPriority);
                 }
                if(priority == null)
                {

                   List<Note> filteredNotesByText = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())).ToList();
                   return Ok(filteredNotesByText);
                }

                List<Note> filteredNotes =   
                    StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())
                         && (int)x.Priority == priority).ToList();
                return Ok(filteredNotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePostNote([FromBody] Note note)
        {
            try
            {
                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("Each note must contain text!");
                }

                if(note.Tags == null || note.Tags.Count == 0)
                {
                    return BadRequest("All notes must have some tags!");
                }

                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost ("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                if (string.IsNullOrEmpty(addNoteDto.Text))
                {
                    return BadRequest("Each note must contain text!");
                }

                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == addNoteDto.UserId);
                if(userDb == null)
                {
                    return NotFound($"User with id {addNoteDto.UserId} was not found");
                }

                List<Tag> tags = new List<Tag>();
                //1,2
                foreach(var tagId in addNoteDto.TagIds)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if(tagDb == null)
                    {
                        return NotFound($"Tag with id {tagId} was not found");
                    }

                    tags.Add(tagDb);
                }

                //create
                Note newNote = new Note
                {
                    Id = StaticDb.Notes.Count + 1,
                    Text = addNoteDto.Text,
                    Priority = addNoteDto.Priority,
                    User = userDb,
                    UserId = userDb.Id, //addNoteDto.UserId
                    Tags = tags
                };

                StaticDb.Notes.Add(newNote); //write in db

                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("userAgent")]
        public IActionResult GetUserAgendFromHeader([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok(userAgent);
        }

        [HttpGet("languageHeader")]
        public IActionResult GetApplicationLanguageFromHeader([FromHeader]  string language)
        {
            return Ok(language);
        }

        [HttpPut("updateNote/{index}")]
        public IActionResult UpdateNote(int index, [FromBody] Tag tag)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("The index cannot be negative!");
                }

                if(index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                var noteDb = StaticDb.Notes[index];

                //if(noteDb.Tags == null)
                //{
                //    noteDb.Tags = new List<Tag>();
                //}

                noteDb.Tags.Add(tag);
                return StatusCode(StatusCodes.Status204NoContent, "Note updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("user/{userId}")]
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
