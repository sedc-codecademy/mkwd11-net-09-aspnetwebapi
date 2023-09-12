using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Models;
using Notes.Services.Service;

namespace Notes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")] // api/user/login
        public IActionResult Login(UserLoginModel model)
        {
            return Ok(userService.Login(model.Username, model.Password));
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            return Ok(userService.Register(model.Email, model.Password, model.Name));
        }
    }
}
