using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() //https://localhost:7034/api/notes
        {
            try
            {
                return Ok(StaticDb.Notes);
            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend, please try again");
            }
        }

        [HttpGet("{id}")] //https://localhost:7034/api/notes/1
        public IActionResult GetById(int id)
        {
            try
            {
                var note = StaticDb.Notes.FirstOrDefault(x => x.Id == id);

                if(note == null)
                {
                    return NotFound($"A Note with Id {id} was not found");
                }

                return Ok(note);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happend, please try again");
            }
        }

        [HttpGet("filter")]//https://localhost:7034/api/notes/filter
        //https://localhost:7034/api/notes/filter?text=wgymork&priority=3&tagName=work
        public IActionResult FilterNotes(string? text, int? priority, string? tagName)
        {            
            var query = StaticDb.Notes;

            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(x => x.Text.ToLower().Contains(text.ToLower())).ToList(); //{Note3, 3}
            }

            if(priority.HasValue)
            {
                if(priority.Value < (int)Priority.Low || priority.Value > (int)Priority.High)
                {
                    return BadRequest($"The priority has values between {(int)Priority.Low} - {(int)Priority.High}");
                }

                query = query.Where(x => (int)x.Priority == priority.Value).ToList();
            }

            if(!string.IsNullOrEmpty(tagName))
            {
                query = query.Where(x => x.Tags.Any(y => y.Name == tagName)).ToList();
            }

            return Ok(query);
        }
    }
}
