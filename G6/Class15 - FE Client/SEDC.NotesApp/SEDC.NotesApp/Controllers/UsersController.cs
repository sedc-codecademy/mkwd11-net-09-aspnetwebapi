using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Shared;

namespace SEDC.NotesApp.Controllers
{
    [Authorize] //all methods in this controller require token
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous] //client doesn't have to provide a token
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            //call some service
            try
            {
                _userService.RegisterUser(registerUserDto);
                return Ok();
            }
            catch (DataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [AllowAnonymous] //client doesn't have to provide a token
        [HttpPost("login")]
        //the response will contain the token, and the token is a string
        public ActionResult<string> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                string token = _userService.Login(loginUserDto);
                return Ok(token);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }

        }
    }
}
