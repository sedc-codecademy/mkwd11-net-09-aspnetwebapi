using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesAppG5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes);
          //  return Ok(StaticDb.SimpleNotes);
        }

        [HttpGet("{index}")] 
        //http://localhost:[port]/api/Notes/1
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if(index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value!");
                }

                if (index >= StaticDb.SimpleNotes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }

                return StatusCode(StatusCodes.Status200OK, StaticDb.SimpleNotes[index]);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator.");
            }
        }

        [HttpGet("{noteId}/user/{userId}")]  //http://localhost:[port]/api/Notes/1/user/2
        public ActionResult<string> GetNoteByIdAnduserId(int noteId, int userId)
        {
            try
            {
                if (noteId < 0 || userId < 0)
                {
                    //return StatusCode(StatusCodes.Status400BadRequest, "The ids cannot have negative values!");
                    return BadRequest("The ids cannot have negative values!");
                }
                return Ok($"Returning note with id {noteId} for user with id {userId}");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
