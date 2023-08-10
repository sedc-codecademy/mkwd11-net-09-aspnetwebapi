using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsAppG5.Models;

namespace NotesAndTagsAppG5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  //http://loclahost:[port]/api/Notes
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            try
            {
                return Ok(StaticDb.Notes);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{index}")] //http://loclahost:[port]/api/Notes/1
        public ActionResult<Note> GetNoteByIndex(int index)
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
                return Ok(StaticDb.Notes[index]);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("queryString")] //http://loclahost:[port]/api/Notes/queryString?index=1
        public ActionResult<Note> GetByQueryString(int? index)
        {
            try
            {
                if(index == null)
                {
                    return BadRequest("Index is a required parameter!");
                }

                if (index < 0)
                {
                    return BadRequest("The index cannot be negative!");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }
                return Ok(StaticDb.Notes[index.Value]);

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

        [HttpGet("multipleQuery")]
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
    }
}
