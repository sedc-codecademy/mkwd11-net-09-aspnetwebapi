using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private static List<Note> notes = new();


        [HttpGet] // api/v1/note
        public IActionResult GetNotes()
        {
            return Ok(notes);
            // return StatusCode(StatusCodes.Status200OK, notes);
        }

        // api/v1/note/1?name=jovan&name2=jovan2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var note = notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound(); // 404
            }
            return Ok(note); // 200
        }

        [HttpPost] // api/v1/note
        public IActionResult CreateNote(Note note)
        {

            if(new Random().Next(3) == 1)
            {
                return Forbid();
            }
            note.Id = notes.Any() ? notes.Max(x => x.Id) + 1 : 1;
            notes.Add(note);
            return Created("api/v1/note", note);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, Note note)
        {
            var toUpdate = notes.FirstOrDefault(x => x.Id == id);
            if(toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.Title = note.Title;
            toUpdate.Description = note.Description;

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound();
            }
            notes.Remove(note);
            return Ok(note);
        }
    }
}
