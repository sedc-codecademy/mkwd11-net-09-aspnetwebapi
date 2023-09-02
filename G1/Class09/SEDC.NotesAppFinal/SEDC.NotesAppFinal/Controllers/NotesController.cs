using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAppFinal.DataAccess.AdoNetImplementation;
using SEDC.NotesAppFinal.DataAccess.DapperImplementation;
using SEDC.NotesAppFinal.DataAccess.Interfaces;
using SEDC.NotesAppFinal.Domain.Models;
using SEDC.NotesAppFinal.DTOs.NoteDTOs;
using SEDC.NotesAppFinal.Services.Interfaces;

namespace SEDC.NotesAppFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly AdoNetRepository _adoRepository;
        private readonly DapperRepository _dapperRepository;

        public NotesController(INotesService notesService, AdoNetRepository _adoRepository, DapperRepository _dapperRepository)
        {
            _notesService = notesService;
            this._adoRepository = _adoRepository;
            this._dapperRepository = _dapperRepository;
        }

        [HttpPost("Dapper")]
        public async Task<ActionResult> CreateDapper([FromBody] Note entity)
        {
            await _dapperRepository.CreateAsync(entity);

            return Ok("Is nice - Borat");
        }

        [HttpDelete("Dapper/{id}")]
        public async Task<ActionResult> DeleteDapper(int id)
        {
            await _dapperRepository.DeleteAsync(id);

            return Ok("Is nice - Borat");
        }

        [HttpGet("Dapper/{id}")]
        public async Task<ActionResult> GetSingleDapper(int id)
        {
            return Ok(await _dapperRepository.GetByIdAsync(id));
        }

        [HttpGet("Dapper")]
        public async Task<ActionResult<Note>> GetDapper()
        {
            return Ok(await _dapperRepository.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdoNet([FromBody] Note entity)
        {
            await _adoRepository.CreateAsync(entity);

            return Ok("Is nice - Borat");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdoNet(int id)
        {
            await _adoRepository.DeleteAsync(id);

            return Ok("Is nice - Borat");
        }

        [HttpGet("adoNet")]
        public async Task<ActionResult<Note>> GetAdoNet()
        {
            return Ok(await _adoRepository.GetAllAsync());
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
    }
}
