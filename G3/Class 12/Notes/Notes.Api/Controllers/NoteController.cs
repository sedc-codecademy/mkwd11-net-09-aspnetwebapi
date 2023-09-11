using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Services.Models;
using Notes.Services.Service;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpGet] // api/v1/note?title=[SomeValue]
        public async Task<IActionResult> GetNotes([FromQuery] SearchNotesModel model)
        {
            return Ok(noteService.GetNotes(new ClaimsPrincipalWrapper(User), model));
        }

        // api/v1/note/1?name=jovan&name2=jovan2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(noteService.GetNote(new ClaimsPrincipalWrapper(User), id));
        }

        [HttpPost] // api/v1/note
        public IActionResult CreateNote([FromBody] CreateNoteModel note)
        {
            NoteModel created = noteService.Create(new ClaimsPrincipalWrapper(User), note);
            return Created("api/v1/note", created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, EditNoteModel edit)
        {
            edit.Id = id;
            return Ok(noteService.Update(new ClaimsPrincipalWrapper(User), edit));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAsync(int id)
        {
            return Ok(noteService.Delete(new ClaimsPrincipalWrapper(User), id));
        }

        [HttpPost("{id}/tags")]
        public IActionResult CreateTag(int id, [FromBody] string tagName)
        {
            var tag = noteService.AddTag(id, tagName);

            return Created($"api/v1/note/{id}/tags", tag);
        }

        [HttpPut("{id}/tags/{tagId}")]
        public IActionResult UpdateTag(int id, int tagId, [FromBody] string name)
        {
            return Ok(noteService.UpdateTag(id, tagId, name));
        }

        [HttpDelete("{id}/tags/{tagId}")]
        public IActionResult DeleteTag(int id, int tagId)
        {
            return Ok(noteService.RemoveTag(id, tagId));
        }
    }
}
