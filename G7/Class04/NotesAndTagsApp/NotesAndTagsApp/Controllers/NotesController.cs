using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Models;
using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<NoteDto>> Get() //https://localhost:7034/api/notes
        {
            try
            {

                var notesDb = StaticDb.Notes;
                var notes = notesDb.Select(x => new NoteDto
                {
                    Priority = x.Priority,
                    TagNames = x.Tags.Select(x=>x.Name).ToList(),
                    Text = x.Text,
                });

                return Ok(notes);
            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happened, please try again");
            }
        }

        [HttpGet("{id}")] //https://localhost:7034/api/notes/1
        public ActionResult<NoteDto> GetById(int id)
        {
            try
            {
                var note = StaticDb.Notes.FirstOrDefault(x => x.Id == id);

                if(note == null)
                {
                    return NotFound($"A Note with Id {id} was not found");
                }

                NoteDto noteDto = new NoteDto
                {
                    Priority = note.Priority,
                    TagNames = note.Tags.Select(x => x.Name).ToList(),
                    Text = note.Text,
                };

                return Ok(noteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error happened, please try again");
            }
        }

        [HttpGet("filter")]//https://localhost:7034/api/notes/filter
        //https://localhost:7034/api/notes/filter?text=wgymork&priority=3&tagName=work
        public ActionResult<List<NoteDto>> FilterNotes(string? text, int? priority, string? tagName)
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

            var notes = query.Select(x => new NoteDto
            {
                Priority = x.Priority,
                TagNames = x.Tags.Select(x => x.Name).ToList(),
                Text = x.Text,
            });

            return Ok(notes);
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            try
            {
                if(string.IsNullOrEmpty(addNoteDto.Text))
                {
                    return BadRequest("Text is a required field");
                }

                if ((int)addNoteDto.Priority < (int)Priority.Low || (int)addNoteDto.Priority > (int)Priority.High)
                {
                    return BadRequest($"The priority has values between {(int)Priority.Low} - {(int)Priority.High}");
                }

                if (addNoteDto.TagIds.Count == 0)
                {
                    return BadRequest("Notes must contain atleast 1 Tag");
                }

                List<Tag> tags = new List<Tag>();
                
                foreach(int tagId in addNoteDto.TagIds)
                {
                    Tag tag = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if(tag is null)
                    {
                        return NotFound($"Tag with id {tagId} was not found!");
                    }
                    tags.Add(tag);
                }

                Note newNote = new Note
                {    
                    Id = ++StaticDb.NoteId,
                    Priority = addNoteDto.Priority,
                    Tags = tags,
                    Text = addNoteDto.Text
                };

                StaticDb.Notes.Add(newNote);

                return StatusCode(StatusCodes.Status201Created, "Note Created");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpPut("updateNote")]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {

            try
            {
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == updateNoteDto.Id);
                if(noteDb is null)
                {
                    return NotFound($"Note with id {updateNoteDto.Id} was not found!");
                }

                if (string.IsNullOrEmpty(updateNoteDto.Text))
                {
                    return BadRequest("Text is a required field");
                }

                if ((int)updateNoteDto.Priority < (int)Priority.Low || (int)updateNoteDto.Priority > (int)Priority.High)
                {
                    return BadRequest($"The priority has values between {(int)Priority.Low} - {(int)Priority.High}");
                }

                if(updateNoteDto.TagIds.Count == 0)
                {
                    return BadRequest("Notes must contain atleast 1 Tag");
                }

                List<Tag> tags = new List<Tag>();

                foreach (int tagId in updateNoteDto.TagIds)
                {
                    Tag tag = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if (tag is null)
                    {
                        return NotFound($"Tag with id {tagId} was not found!");
                    }
                    tags.Add(tag);
                }

                noteDb.Tags = tags;
                noteDb.Text = updateNoteDto.Text;
                noteDb.Priority = updateNoteDto.Priority;

                return StatusCode(StatusCodes.Status204NoContent, "Note updated");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpDelete("deleteNote")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Id is not valid");
                }

                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if(noteDb is null)
                {
                    return NotFound($"Note with id {id} not found!");
                }

                StaticDb.Notes.Remove(noteDb);

                return Ok($"Note with id {id} was deleted");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

    }
}
