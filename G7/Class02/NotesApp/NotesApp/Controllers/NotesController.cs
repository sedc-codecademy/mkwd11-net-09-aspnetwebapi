using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase //https://localhost:[port]/api/notes
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            var notes = StaticDb.Notes;

            //return notes;
            return StatusCode(StatusCodes.Status200OK, notes);
        }

        [HttpGet("index")] //https:localhost:[port]/api/notes/index
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    //return StatusCode(StatusCodes.Status400BadRequest, "The value for index cannot be negative number");
                    return BadRequest("The value for index cannot be negative number");
                }

                if (index >= StaticDb.Notes.Count) //if(index ? StaticDb.Notes.Count - 1)
                {
                    //return StatusCode(StatusCodes.Status404NotFound, $"The element on index {index} does not exist");
                    return NotFound($"The element on index {index} does not exist");
                }

                //return StatusCode(StatusCodes.Status200OK, StaticDb.Notes[index]);
                return Ok(StaticDb.Notes[index]);
            } catch (Exception ex)
            {
                //Log the error, send alarm, send emails
                return StatusCode(StatusCodes.Status500InternalServerError, "An error ocurred, try again later");
            }
        }

        [HttpGet("{noteId}/user/{userId}")] //https:localhost:[port]/api/notes/noteId/user/userId
        //Marko as user has UserId 3, and wants to get his note with NoteId 7
        //https:localhost:[port]/api/notes/7/user/3
        public ActionResult<string> GetNoteByIdAndUserId(int noteId, int userId)
        {
            if(noteId < 0 || userId < 0)
            {
                //return BadRequest("NoteId or UserId cannot be negative");
                //return "NoteId or UserId cannot be negative";
            }

            return Ok($"Returning the note with id {noteId} for user with userId {userId}");
        }

        //[HttpPost] //Sync way of reading the body
        //public IActionResult AddNewNote()
        //{
        //    try
        //    {
        //        using (StreamReader reader = new StreamReader(Request.Body))
        //        {
        //            string note = reader.ReadToEnd();

        //            if (note == null || note == string.Empty)
        //            {
        //                return BadRequest("The note should have some value");
        //            }

        //            StaticDb.Notes.Add(note);
        //            return Ok($"New note: '{note}' has been added");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error ocurred, try again later");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> AddNewNote()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string note = await reader.ReadToEndAsync();

                    if (note == null || note == string.Empty)
                    {
                        return BadRequest("The note should have some value");
                    }

                    StaticDb.Notes.Add(note);
                    return Ok($"New note: '{note}' has been added");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error ocurred, try again later");
            }
        }
    }
}
