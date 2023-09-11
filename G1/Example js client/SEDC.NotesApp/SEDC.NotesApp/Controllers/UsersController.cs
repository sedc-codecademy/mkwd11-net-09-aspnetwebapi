using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using System;

namespace SEDC.NotesApp.Controllers
{
    [Authorize] //all actions require authentication
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpPost("register")]
        [AllowAnonymous] //the user that sends the request can be unauthenticated
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _usersService.Register(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User registered!");
            }
            catch(UserException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch(Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous] //the user that sends the request can be unauthenticated
        public ActionResult<string> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                string token = _usersService.Login(loginDto);
                return Ok(token);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }
    }
}
