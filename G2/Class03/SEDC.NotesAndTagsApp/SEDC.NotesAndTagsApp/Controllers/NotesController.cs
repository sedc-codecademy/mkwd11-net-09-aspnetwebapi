using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAndTagsApp.Models;

namespace SEDC.NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //https://localhost:[port]/api/notes
        public ActionResult<List<Note>> Get()
        {
            try
            {
                return Ok(StaticDb.Notes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        //ROUTE PARAMETERS 

        [HttpGet("index/{index}")] //https://localhost:[port]/api/notes/index/1
        public IActionResult GetNoteByIndex([FromRoute] int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }

                if (index >= StaticDb.Notes.Count)
                {
                    return BadRequest($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index]);
            }
            catch (Exception)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("filter/{filter}/priority/{priority}")] //https://localhost:[port]/api/notes/filter/home/priority/3
        public IActionResult FilterNotes([FromRoute] string filter,
                                         [FromRoute] int priority)
        {
            try
            {
                if (string.IsNullOrEmpty(filter) || priority <= 0)
                {
                    return BadRequest("Filter parameters are required!");
                }

                if (priority > 3)
                {
                    return BadRequest("Invalid value for priority!");
                }

                var notesFromDb = StaticDb.Notes.Where(note =>
                        note.Text.ToLower().Contains(filter.ToLower())
                            && (int)note.Priority == priority).ToList();

                return Ok(notesFromDb);
            }
            catch (Exception)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        //QUERY PARAMETERS 
        [HttpGet("queryString")] //https://localhost:[port]/api/notes/queryString?index=1
        public IActionResult GetByQueryString([FromQuery] int? index)
        {
            try
            {
                if (index is null)
                {
                    return BadRequest();
                }

                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }

                if (index >= StaticDb.Notes.Count)
                {
                    return BadRequest($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Notes[index.Value]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("multipleQueryParameters")] //https://localhost:[port]/api/notes/multipleQueryParameters?filter=home&priority=3
        public IActionResult MultipleQueryParameters([FromQuery] string? filter, 
                                                     [FromQuery] int? priority) 
        {
            try
            {
                if (string.IsNullOrEmpty(filter) || priority is null)
                {
                    return BadRequest("Filter parameters are required!");
                }

                if (priority > 3)
                {
                    return BadRequest("Invalid value for priority!");
                }

                var notesFromDb = StaticDb.Notes.Where(note =>
                        note.Text.ToLower().Contains(filter.ToLower())
                            && (int)note.Priority == priority).ToList();

                return Ok(notesFromDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        //COMBINDED ROUTE AND QUERY PARAMETERS

        [HttpGet("index/{index}/name/{name}")] //https://localhost:7205/api/notes/index/1/name/viktor?pageNumber=2&language=en
        public IActionResult CombineRouteAndQuery([FromRoute] string index,
                                                  [FromRoute] string name,
                                                  [FromQuery] int? pageNumber,
                                                  [FromQuery] string? language) 
        {
            return Ok();
        }

        //HEADER PARAMETERS

        [HttpGet("header")]
        public IActionResult GetHeader([FromHeader(Name = "TestHeader")] string testHeader) 
        {
            return Ok(testHeader);
        }


        [HttpGet("userAgent")]
        public IActionResult GetUserAgentHeader([FromHeader(Name = "User-Agent")] string userAgent) 
        {
            return Ok(userAgent);
        }

        //BODY PARAMETERS

        [HttpPost]
        public IActionResult PostNote([FromBody] Note note) 
        {
            try
            {
                if (string.IsNullOrEmpty(note.Text)) 
                {
                    return BadRequest("Note text must not be empty");
                }

                if (note.Tags == null || note.Tags.Count == 0) 
                {
                    return BadRequest("Note must contain tags.");
                }

                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created, "Note Created");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPut("updateNote/{index}")]
        public IActionResult UpdateNote([FromRoute] int index,
                                        [FromBody] Tag tag) 
        {
            try
            {
                if (index < 0) 
                {
                    return BadRequest("The index can not be negative.");
                }

                if (index >= StaticDb.Notes.Count) 
                {
                    return NotFound($"There is not resource on index {index}");
                }

                var note = StaticDb.Notes[index];
                note.Tags.Add(tag);

                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

    }
}
