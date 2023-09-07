using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.TestAPI.Models;

namespace SEDC.TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("testUser")]
        public ActionResult<User> GetTestUser()
        {
            User user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Username = "testUser"
            };
            return Ok(user);
        }


        [HttpPost("addTestUser")]
        public IActionResult AddTestUser([FromBody] User user)
        {
            return Ok();
        }
    }
}
