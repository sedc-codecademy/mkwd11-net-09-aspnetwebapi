using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAppFinal.DTOs.UserDtos;
using SEDC.NotesAppFinal.Services.Interfaces;

namespace SEDC.NotesAppFinal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            try
            {
                await _userService.RegisterUser(user);
                return StatusCode(StatusCodes.Status201Created, "User created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto user)
        {
            try
            {
                string token = await _userService.LoginUserAsync(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }
    }
}
