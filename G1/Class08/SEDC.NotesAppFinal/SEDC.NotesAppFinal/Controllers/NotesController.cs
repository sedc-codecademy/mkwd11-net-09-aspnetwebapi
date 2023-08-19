using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAppFinal.DTOs.NoteDTOs;
using SEDC.NotesAppFinal.Services.Interfaces;

namespace SEDC.NotesAppFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNoteAsync(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Id can not be null");
                }

                if (id <= 0)
                {
                    return BadRequest("Invalid input for Id");
                }

                NoteDto noteDto = await _notesService.GetNoteAsync(id);

                if (noteDto == null)
                {
                    return NotFound($"Note with Id: {id} not found");
                }

                return Ok(noteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteDto>>> GetAllNotesAsync()
        {
            try
            {
                return Ok(await _notesService.GetAllNotesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateNoteAsync([FromBody] CreateNoteDto createNoteDto)
        {
            try
            {
                if(createNoteDto == null || createNoteDto.UserId == 0 || createNoteDto.Text == null || createNoteDto.Tag == 0 || createNoteDto.Priority == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _notesService.CreateNoteAsync(createNoteDto);

                return StatusCode(StatusCodes.Status201Created, "Note added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }
    }
}
