using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Entities;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/Notes
        public ActionResult<List<string>> Get()
        {
            return Ok(StaticDb.SimpleNotes);
        }

        [HttpGet("{index?}")] //http://localhost:[port]/api/Notes/{index}
        public ActionResult<string> GetByIndex(int? index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index is a negative number.");
                }

                if (index >= StaticDb.SimpleNotes.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"The note with index {index} was not found.");
                }

                return Ok(StaticDb.SimpleNotes[(int)index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. Contact the support team.");
            }
        }

        [HttpGet("{noteId}/user/{userId}")] //http://localhost:[port]/api/Notes/{noteId}/user/{userId}
        public ActionResult<string> GetNoteByIdAndUserId(int noteId, int userId)
        {
            if (noteId < 0 || userId < 0)
            {
                return BadRequest("The ids can not be negative.");
            }

            return Ok($"Returning note with id {noteId} and user with id {userId}.");
        }

        [HttpPost] //http://localhost:[port]/api/Notes POST METHOD !!!
        public IActionResult Post(NoteModel noteModel)
        {
            try
            {
                if (string.IsNullOrEmpty(noteModel.Text))
                {
                    return BadRequest("The body of the request can not be empty.");
                }

                StaticDb.SimpleNotes.Add(noteModel.Text);
                return StatusCode(StatusCodes.Status201Created, "The new note was created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. Contact the support team.");
            }
        }
    }
}
