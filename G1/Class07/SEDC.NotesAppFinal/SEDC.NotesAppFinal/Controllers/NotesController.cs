﻿using Microsoft.AspNetCore.Http;
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
    }
}
