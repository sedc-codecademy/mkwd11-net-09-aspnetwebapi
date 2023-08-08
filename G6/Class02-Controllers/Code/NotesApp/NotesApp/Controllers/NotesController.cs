using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/notes
        public ActionResult<List<string>> GetNotes()
        {
            //return StatusCode(StatusCodes.Status200OK, StaticDb.Notes);
            return Ok(StaticDb.Notes);
        }

        [HttpGet("{index}")] //http://localhost:[port]/api/notes/2
        public ActionResult<string> GetNoteByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index can not be negative");
                    //return BadRequest("The index can not be negative");
                }

                if (index >= StaticDb.Notes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no note on index {index}");
                    //return NotFound($"There is no note on index {index}");
                }

                //throw new Exception();

                return Ok(StaticDb.Notes[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpGet("{noteId}/users/{userId}")] //http://localhost:[port]/api/notes/1/users/2
        public ActionResult<string> GetNoteForUser(int noteId, int userId)
        {
            if(noteId < 0 || userId < 0)
            {
                return BadRequest("Ids can not be negative");
            }

            return $"Returning info for note with id {noteId} and for user with id {userId}";
        }

        [HttpPost]
        public IActionResult PostNote()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(Request.Body))
                {
                    string newNote = streamReader.ReadToEnd();
                    if (string.IsNullOrEmpty(newNote))
                    {
                        return BadRequest();
                    }

                    StaticDb.Notes.Add(newNote);
                    return StatusCode(StatusCodes.Status201Created, "Note successfully added");
                    //return Created("Note successfully added", newNote);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

    }
}
