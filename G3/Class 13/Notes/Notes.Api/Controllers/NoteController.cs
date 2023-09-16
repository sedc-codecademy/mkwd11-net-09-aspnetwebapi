using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Notes.Services.Models;
using Notes.Services.Service;
using Notes.Services.Service.External;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;
        private readonly ILogger logger;
        private readonly IProfileService profileService;

        public NoteController(INoteService noteService, ILogger<NoteController> logger, IProfileService profileService)
        {
            this.noteService = noteService;
            this.logger = logger;
            this.profileService = profileService;
            logger.LogDebug("This is debug");
            logger.LogTrace("This is trace");
            logger.LogInformation("This is information");
            logger.LogWarning("Warning");
            logger.LogError("Error");
            logger.LogCritical("Critical");
        }
        [HttpGet]
        public async Task<IActionResult> GetNotes([FromQuery] SearchNotesModel model)
        {
            return Ok(noteService.GetNotes(new ClaimsPrincipalWrapper(User), model));
        }

        // api/v1/note/1?name=jovan&name2=jovan2
        // api/v1/note?title=[SomeValue]
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
        [HttpGet("profiles")]
        public async Task<IActionResult> GetProfiles()
        {
            string token = Request.Headers[HeaderNames.Authorization];
            var result = await profileService.GetProfiles(token.Replace("Bearer ", ""));
            return Ok(result);
        }
    }
}
