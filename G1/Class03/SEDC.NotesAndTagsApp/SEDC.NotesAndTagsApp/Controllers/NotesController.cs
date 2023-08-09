using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAndTagsApp.Models;

namespace SEDC.NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            return Ok(StaticDb.Notes);
        }

        [HttpGet("{index}")]
        public ActionResult<Note> GetSingle(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("Index can not be a negative value.");
                }

                return Ok(StaticDb.Notes[index]);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Argument out of range, there is no index with that number");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A server error occured, please contact the support team.");
            }
        }

        [HttpGet("queryString")]
        public ActionResult<Note> GetByQueryString(int? index)
        {
            try
            {
                if (index == null)
                {
                    return BadRequest("Index is a required paramter.");
                }
                if (index < 0)
                {
                    return BadRequest("Index can not be a negative value.");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource with an index of {index}");
                }

                return Ok(StaticDb.Notes[index.Value]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A server error occured, please contact the support team.");
            }
        }

        [HttpGet("{text}/priority/{priority}")]
        public ActionResult<List<Note>> FilterNotes(string text, int priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text) || priority <= 0)
                {
                    return BadRequest("Filter parameters are required");
                }
                if (priority > 3)
                {
                    return BadRequest("Priority must be between 1 and 3.");
                }

                List<Note> notes = StaticDb.Notes.Where(x => x.Text.ToLower() == text.ToLower() && (int)x.Priority == priority).ToList();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A server error occured, please contact the support team.");
            }
        }

        [HttpGet("multipleQueryParams")]
        //http://localhost:5263/api/Notes/multipleQueryParams?priority=2

        //http://localhost:5263/api/Notes/multipleQueryParams?text=&priority=2

        //http://localhost:5263/api/Notes/multipleQueryParams?text=&priority=

        //http://localhost:5263/api/Notes/multipleQueryParams?text=Learn%20api

        //http://localhost:5263/api/Notes/multipleQueryParams?priority=2

        //http://localhost:5263/api/Notes/multipleQueryParams?text=Learn+JS&priority=2

        //http://localhost:5263/api/Notes/multipleQueryParams?text=Learn+JS

        //http://localhost:5263/api/Notes/multipleQueryParams?priority=1&text=Learn+JS

        public ActionResult<List<Note>> FilterNotesByMultipleQueryParams(string? text, int? priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text) && priority == null)
                {
                    return BadRequest("Insert at least one query paramater");
                }
                if (string.IsNullOrEmpty(text))
                {
                    return Ok(StaticDb.Notes.Where(x => (int)x.Priority == priority).ToList());
                }
                if (priority == null)
                {
                    return Ok(StaticDb.Notes.Where(x => x.Text.ToLower() == text.ToLower()).ToList());
                }

                return Ok(StaticDb.Notes.Where(x => x.Text.ToLower() == text.ToLower() && (int)x.Priority == priority).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A server error occured, please contact the support team.");
            }
        }

        [HttpGet("header")]
        public IActionResult GetHeader([FromHeader(Name = "TestHeader")] string testHeader)
        {
            return Ok(testHeader);
        }

        [HttpGet("useragent")]
        public IActionResult GetHeaderUserAgent([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Ok(userAgent);
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] Note note)
        {
            try
            {
                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("Insert some text.");
                }
                if (note.Tags == null || note.Tags.Count == 0)
                {
                    return BadRequest("Insert some tags.");
                }
                if (note.Priority == null)
                {
                    return BadRequest("Insert some priority");
                }

                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A server error occured, please contact the support team.");
            }
        }

        [HttpPatch("edit/{index}")]
        public IActionResult EditNote([FromBody] Note note, int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("Index can not be a negative value.");
                }
                if (index >= StaticDb.Notes.Count)
                {
                    return NotFound($"There is no resource with an index of {index}");
                }

                Note noteDb = StaticDb.Notes[index];

                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("Insert some text.");
                }
                if (note.Tags == null || note.Tags.Count == 0)
                {
                    return BadRequest("Insert some tags.");
                }
                if (note.Priority == null)
                {
                    return BadRequest("Insert some priority");
                }

                noteDb.Text = note.Text;
                noteDb.Tags = note.Tags;
                noteDb.Priority = note.Priority;

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A server error occured, please contact the support team.");
            }
        }
    }
}
