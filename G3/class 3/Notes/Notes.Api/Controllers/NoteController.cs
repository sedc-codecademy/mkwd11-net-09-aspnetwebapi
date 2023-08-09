using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Data;
using Notes.Api.Domain;
using Notes.Api.Models;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NotesDbContext context;

        public NoteController(NotesDbContext context)
        {
            this.context = context;
        }

        [HttpGet] // api/v1/note?title=[SomeValue]
        public async Task<IActionResult> GetNotes([FromQuery] SearchNotesModel model)
        {
            IQueryable<Note> toReturn = context.Notes;
            if (!string.IsNullOrEmpty(model.Title))
            {
                toReturn = toReturn
                    .Where(x => x.Title.Contains(model.Title));
            }

            if (!string.IsNullOrEmpty(model.Description))
            {
                toReturn = toReturn
                    .Where(x => x.Description.Contains(model.Description));
            }
            var result = await toReturn.Include(x => x.Tags).AsNoTracking().ToListAsync().ConfigureAwait(false);
            return Ok(result);
            // return StatusCode(StatusCodes.Status200OK, notes);
        }

        // api/v1/note/1?name=jovan&name2=jovan2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var note = context.Notes.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound(); // 404
            }
            return Ok(note); // 200
        }

        [HttpPost] // api/v1/note
        public async Task<IActionResult> CreateNoteAsync([FromBody] string note)
        {
            context.Notes.Add(new Note
            {
                Title = note,
            });
            await context.SaveChangesAsync();
            return Created("api/v1/note", note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNoteAsync(int id, Note note)
        {
            var toUpdate = await context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if(toUpdate == null)
            {
                return NotFound();
            }

            toUpdate.Title = note.Title;
            toUpdate.Description = note.Description;
            await context.SaveChangesAsync();
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var note = await context.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if(note == null)
            {
                return NotFound();
            }
            context.Notes.Remove(note);
            await context.SaveChangesAsync();
            return Ok(note);
        }

        [HttpGet("{id}/tags")] //api/v1/note
        public IActionResult GetTags(int id)
        {
            var note = context
                .Notes
                .Include(x => x.Tags)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound();
            }
            return Ok(note.Tags);
        }

        [HttpPost("{id}/tags")]
        public async Task<IActionResult> CreateTag(int id, [FromBody] Tag tag)
        {
            if (string.IsNullOrEmpty(tag.Name))
            {
                return BadRequest();
            }

            var note = await context
                .Notes
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
            if(note == null)
            {
                return NotFound();
            }

            if(note.Tags.Any(x => x.Name == tag.Name))
            {
                return BadRequest("Can not create duplicate tags");
            }
            note.Tags.Add(tag);
            await context.SaveChangesAsync();

            return Created($"api/v1/note/{note.Id}/tags", tag);
        }

        [HttpPut("{id}/tags/{tagId}")]
        public async Task<IActionResult> UpdateTagAsync(int id, int tagId, [FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var note = context
                .Notes
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == id);
            if(note == null)
            {
                return NotFound("Note doesn't exist");
            }
            var tag =  note.Tags.FirstOrDefault(x => x.Id == tagId);

            if(tag == null)
            {
                return NotFound("Tag doesn't exist");
            }

            tag.Name = name;
            await context.SaveChangesAsync();
            return Ok(tag);
        }

        [HttpDelete("{id}/tags/{tagId}")]
        public async Task<IActionResult> DeleteTag(int id, int tagId)
        {
            var note = await context
                .Notes
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
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
            await context.SaveChangesAsync();
            return Ok(tag);
        }
    }
}
