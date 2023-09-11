using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SEDC.NotesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INotesService _noteService;

        public NotesController(INotesService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        
        public ActionResult<List<NoteDto>> Get()
        {
            try
            {
                var claims = User.Claims;
                string userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string username = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                string userFullName = User.Claims.First(x => x.Type == "userFullName").Value;
                if(username != "superAdmin")
                {
                    Log.Error("The user is not with username superadmin");
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                Log.Information("Successfully retrieved notes information");
                return _noteService.GetAllNotes();
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")] //this action can be accessed by a user with Role = Admin, token with Role Claim = Admin
        public ActionResult<NoteDto> GetById(int id)
        {
            try
            {
                return _noteService.GetNoteById(id);
            }
            catch(ResourceNotFoundException e)
            {
                Log.Error($"Note with id {id} was not found");
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddUpdateNoteDto addNoteDto)
        {
            try
            {
                _noteService.AddNote(addNoteDto);
                Log.Information("Note was successfully added");
                return StatusCode(StatusCodes.Status201Created, "Note created successfully!");
            }
            catch(NoteException e)
            {
                Log.Warning("Wrong data for new note was sent!");
                Log.Warning(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] AddUpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.UpdateNote(updateNoteDto);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (NoteException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpDelete("{id}")] //api/notes
        public IActionResult Delete(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid id value!");
                }
                _noteService.DeleteNote(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }
    }
}
