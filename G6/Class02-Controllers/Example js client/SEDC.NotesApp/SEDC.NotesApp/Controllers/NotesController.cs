using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/notes
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/notes
        public ActionResult<List<string>> Get()
        {
            //return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes);
            return Ok(StaticDb.SimpleNotes);
        }

        [HttpGet("{index}")] ////http://localhost:[port]/api/notes/1
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value");
                }

                //throw new Exception();

                if (index >= StaticDb.SimpleNotes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes[index]);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }

        [HttpGet("{noteId}/user/{userId}")] //http://localhost:[port]/api/notes/1/user/2
        public ActionResult<string> GetNoteByIdAndUserId(int noteId, int userId)
        {
            //return StaticDb.SimpleNotes[8];
            if(noteId < 0 || userId < 0)
            {
                //return StatusCode(StatusCodes.Status400BadRequest, "The ids can not be negative!");
                return BadRequest("The ids can not be negative!");
                //return "Invalid values";
            }

            //return StatusCode(StatusCodes.Status200OK, $"Returning note with id {noteId} for user with id {userId}");
            return Ok($"Returning note with id {noteId} for user with id {userId}");
        }

        //This is optional - use Postman, Postman should be presented at Class 03
        //but feel free to use it at Class 02
        [HttpPost] //http://localhost:[port]/api/notes
        public IActionResult Post()
        {
            try
            {
                //Request (ready class) -> Http Request which was sent to the action
                using(StreamReader reader = new StreamReader(Request.Body))
                {
                    string newNote = reader.ReadToEnd();

                    if (string.IsNullOrEmpty(newNote))
                    {
                        return BadRequest("The body of the request can not be empty");
                    }

                    StaticDb.SimpleNotes.Add(newNote);
                    return StatusCode(StatusCodes.Status201Created, "The new note was added");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }
    }
}
