using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Domain;
using Notes.Api.Models;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private static List<Note> notes = new();


        [HttpGet] // api/v1/note?title=[SomeValue]
        public IActionResult GetNotes([FromQuery] SearchNotesModel model)
        {
            var toReturn = notes;
            if (!string.IsNullOrEmpty(model.Title))
            {
                toReturn = notes
                    .Where(x => x.Title.Contains(model.Title))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(model.Description))
            {
                toReturn = toReturn
                    .Where(x => x.Description?.Contains(model.Description) ?? false)
                    .ToList();
            }
            return Ok(toReturn);
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

        [HttpGet("{id}/tags")] //api/v1/note
        public IActionResult GetTags(int id)
        {
            var note = notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound();
            }
            return Ok(note.Tags);
        }

        [HttpPost("{id}/tags")]
        public IActionResult CreateTag(int id, [FromBody] Tag tag)
        {
            if (string.IsNullOrEmpty(tag.Name))
            {
                return BadRequest();
            }

            var note = notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound();
            }

            if(note.Tags.Any(x => x.Name == tag.Name))
            {
                return BadRequest("Can not create duplicate tags");
            }
            note.Tags.Add(tag);

            return Created($"api/v1/note/{note.Id}/tags", tag);
        }

        [HttpPut("{id}/tags/{tagId}")]
        public IActionResult UpdateTag(int id, int tagId, [FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var note = notes.FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound("Note doesn't exist");
            }
            var tag = note.Tags.FirstOrDefault(x => x.Id == tagId);

            if(tag == null)
            {
                return NotFound("Tag doesn't exist");
            }

            tag.Name = name;
            return Ok(tag);
        }

        [HttpDelete("{id}/tags/{tagId}")]
        public IActionResult DeleteTag(int id, int tagId)
        {
            var note = notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                return NotFound("Note doesn't exist");
            }
            var tag = note.Tags.FirstOrDefault(x => x.Id == tagId);

            if (tag == null)
            {
                return NotFound("Tag doesn't exist");
            }
            note.Tags.Remove(tag);
            return Ok(tag);
        }
    }
}
