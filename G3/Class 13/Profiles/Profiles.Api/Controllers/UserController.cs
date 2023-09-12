using Microsoft.AspNetCore.Mvc;
using Profiles.BLL.Models;
using Profiles.BLL.Services;

namespace Profiles.Api.Controllers
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

        [HttpPost("register")]
        public IActionResult Register(UserModel model)
        {
            userService.Register(model);
            return Ok();
        }
        [HttpPost("login")]
        public IActionResult Login(UserLoginModel model)
        {
            return Ok(userService.Login(model));
        }
    }
}
