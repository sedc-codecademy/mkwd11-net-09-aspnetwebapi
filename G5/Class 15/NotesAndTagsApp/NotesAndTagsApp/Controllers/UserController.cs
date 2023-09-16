using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Services.Interfaces;
using Serilog;

namespace NotesAndTagsApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [AllowAnonymous] //no token needed (we can not be logged in before registration)
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                Log.Information($"Registration model info: FirstName: {registerUserDto.FirstName}, LastName: {registerUserDto.LastName}, Username: {registerUserDto.Username}");

                _userService.Register(registerUserDto);

                Log.Information($"Successfully registered {registerUserDto.Username}");

                return StatusCode(StatusCodes.Status201Created, "User was created");
            }
            catch(Exception ex)
            {
                Log.Error($"Internal exception: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous] //no token needed (we can not be logged in before login)
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDto loginDto)
        {
            try
            {
                string token = _userService.LoginUser(loginDto);
                Log.Information($"Successfully login: {loginDto.Username}");
                return Ok(token);
            }
            catch (Exception e)
            {
                Log.Error($"Internal exception: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
