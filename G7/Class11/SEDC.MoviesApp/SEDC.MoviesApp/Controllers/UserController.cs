using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MoviesApp.Dtos.Users;
using SEDC.MoviesApp.Services;
using SEDC.MoviesApp.Shared;

namespace SEDC.MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _userService.Register(registerUserDto);

                return Ok("User registered");

            }
            catch(UserException ex)
            {
                return BadRequest(ex.Message);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred on the server");
            }

        }

        [HttpPost("login")]
        public ActionResult<UserDto> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var user = _userService.Login(loginUserDto);

                return Ok(user);

            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred on the server");
            }

        }

    }
}
