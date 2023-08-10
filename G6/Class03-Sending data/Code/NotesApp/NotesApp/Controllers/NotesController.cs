using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            try
            {
                return Ok(StaticDb.Notes);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

        [HttpGet("details/{id}")] //http://localhost:[port]/api/notes/details/1
        //document which status codes can be returned by the action
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Note))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Note> GetNoteDetails(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Id can not be negative"); //status code 400
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} was not found"); //status code 404
                }

                return Ok(noteDb); //status code 200
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

        [HttpGet("text/{text}/priority/{priority}")] 
        //http:localhost:[port]/api/notes/text/gym/priority/2
        //find all notes whose text contains gym and with priority medium
        //limitations: text is required, we need fixed parts from the route to make it more clear,
        //we can not switch places of the paramters
        public ActionResult<List<Note>> FilterNotes(string text, int priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return BadRequest("The text field can not be empty");
                }

                if(priority <= 0 || priority > 3)
                {
                    return BadRequest("Invalid priority value");
                }

                //because both params are required, we are using AND
                List<Note> notesDb = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower()) 
                                                &&  (int)x.Priority == priority).ToList();
                return Ok(notesDb);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

        [HttpGet("filterNotes")]
        //http:localhost:[port]/api/notes/filterNotes?text=gym&priority=1
        //http:localhost:[port]/api/notes/filterNotes?priority=1&text=gym
        //http:localhost:[port]/api/notes/filterNotes?priority=1
        //http:localhost:[port]/api/notes/filterNotes?text=gym
        //http:localhost:[port]/api/notes/filterNotes?
        public ActionResult<List<Note>> FilterNotesByQueryParams(string? text, int? priority)
        {
            try
            {
                if(string.IsNullOrEmpty(text) && priority == null)
                {
                    return Ok(StaticDb.Notes);
                }

                //at least one is not empty
                if (string.IsNullOrEmpty(text))
                {
                    //priority has value
                    List<Note> notesDb = StaticDb.Notes.Where(x => (int)x.Priority == priority).ToList();
                    return Ok(notesDb);

                }

                if(priority == null)
                {
                    List<Note> notesDb = StaticDb.Notes.
                        Where(x => x.Text.ToLower().Contains(text.ToLower())).ToList();
                    return Ok(notesDb);
                }

                //we have values for both params
                List<Note> filteredNotes = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())
                                                && (int)x.Priority == priority).ToList();
                return Ok(filteredNotes);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

        [HttpPost]
        public IActionResult AddNote([FromBody] Note note)
        {
            try
            {
                //first check negative scenarios
                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("You must specify text");
                }

                if(note.Tags == null || !note.Tags.Any())
                {
                    return BadRequest("You must specify tags");
                }

                //add to db
                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }


        [HttpPut]
        public IActionResult UpdateNote([FromBody] Note note)
        {
            try
            {
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == note.Id);
                if(noteDb == null)
                {
                    return NotFound($"Note with id {note.Id} was not found");
                }

                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("You must specify text");
                }

                if (note.Tags == null || !note.Tags.Any())
                {
                    return BadRequest("You must specify tags");
                }

                noteDb.Text = note.Text;
                noteDb.Priority = note.Priority;
                noteDb.Tags = note.Tags;

                return NoContent();
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

        [HttpPut("{id}/addTag")]
        public IActionResult AddTag(int id, [FromBody] Tag tag)
        {
            try
            {
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} was not found");
                }

                noteDb.Tags.Add(tag);
                return NoContent();
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

    }
}
