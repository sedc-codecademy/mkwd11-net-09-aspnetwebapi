using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DependencyInjectionExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private ITestService _service;

        public TestController(ITestService service)
        {
            _service = service;
        }

        [HttpGet("1")]
        public IActionResult ServiceTest() 
        {
            //var service = new TestService();

            var result = _service.TestMethodOne(1);

            return Ok(result);
        }

        [HttpGet("2")]
        public IActionResult ServiceTestTwo()
        {
            //var service = new TestService();

            var result = _service.TestMethodTwo("Viktor");

            return Ok(result);
        }
    }
}
