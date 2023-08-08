using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SEDC.NoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet] //https://localhost:[port]/api/notes GET
        public IActionResult Get() 
        {
            var response = StaticDb.SimpleNotes;
            return Ok(response);
        }
    }
}
