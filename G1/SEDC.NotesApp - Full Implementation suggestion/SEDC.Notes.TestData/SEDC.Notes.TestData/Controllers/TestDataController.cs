using Microsoft.AspNetCore.Mvc;

namespace SEDC.Notes.TestData.Controllers
{
    public class TestUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        [HttpGet("testuser")]
        public ActionResult<TestUser> GetTestUser()
        {
            return new TestUser()
            {
                FirstName = "TestFirst1",
                LastName = "TestLast1",
                Username = "TestUsername1",
                Password = "TestPassword1",
                ConfirmPassword = "TestPassword1"
            };
        }
    }
}
