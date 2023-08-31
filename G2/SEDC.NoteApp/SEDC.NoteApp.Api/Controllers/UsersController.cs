using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Services.Abstraction;

namespace SEDC.NoteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto) 
        {
            _userService.RegisterUser(registerUserDto);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto) 
        {
            var token = _userService.LoginUser(loginUserDto);
            return Ok(token);
        }
    }
}
