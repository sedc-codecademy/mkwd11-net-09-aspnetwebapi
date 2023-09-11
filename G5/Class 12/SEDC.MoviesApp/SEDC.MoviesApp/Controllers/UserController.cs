using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MoviesApp.Dtos.UserDto;
using SEDC.MoviesApp.Services.Interfaces;
using SEDC.MoviesApp.Shared;

namespace SEDC.MoviesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDto loginDto)
        {
            try
            {
                string token = _userService.LoginUser(loginDto);
                return Ok(new ResponseDto() { Success = token});
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Error = "An error occured!"});
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                _userService.RegisterUser(registerDto);
                return Ok(new ResponseDto() { Success = "Successfully registered user!" });
            }
            catch (UserException e)
            {
                return BadRequest(new ResponseDto() { Error = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Error = "An error occured!" });
            }

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginUser = _userService.Authenticate(loginDto);
                return Ok(loginUser);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto() { Error = "An error occured!" });

            }
        }

    }
}
