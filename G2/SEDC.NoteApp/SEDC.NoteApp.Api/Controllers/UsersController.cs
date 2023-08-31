using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NoteApp.DTOs;

namespace SEDC.NoteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto) 
        {
            _userService.RegisterUser(registerUserDto);
            return Ok();
        }
    }
}
