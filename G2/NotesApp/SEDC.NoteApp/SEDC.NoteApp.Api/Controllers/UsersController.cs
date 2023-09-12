using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NoteApp.CustomExceptions;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Services.Abstraction;
using Serilog;

namespace SEDC.NoteApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto) 
        {
            try
            {
                Log.Information("Processing registration... Username: '{username}'", registerUserDto?.Username);
                _userService.RegisterUser(registerUserDto);
                Log.Information("User '{firstname}' registered successfully", registerUserDto?.FirstName);
                return StatusCode(201, "User created!");
            }
            catch (UserDataException ex) 
            {
                //Log.Error(ex.Message);
                Log.Warning("User registration failed due to data validation. Username: {username}. Message: {message}", registerUserDto?.Username, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured during user registration! Username: '{username}'", registerUserDto?.Username);
                return StatusCode(500, "An error occurred!");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto) 
        {
            try
            {
                Log.Information("Processing login... User: {username}", loginUserDto.Username);
                var token = _userService.LoginUser(loginUserDto);
                Log.Information("User logged in successfully: {Username}", loginUserDto.Username);
                return Ok(token);
            }
            catch (UserDataException ex)
            {
                Log.Warning("User login failed due to data validation. Username: '{Username}'. Message: {Message}", loginUserDto.Username, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during user login attempt! Username: '{Username}'", loginUserDto.Username);
                return StatusCode(500, "An error occurred!");
            }
        }
    }
}
