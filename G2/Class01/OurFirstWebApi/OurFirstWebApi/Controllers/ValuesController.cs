using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OurFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("GetStrings")]
        public List<string> GetStrings() 
        {
            return new List<string> { "value1", "value2" };
        }

        [HttpPost("GetStringsPost")]
        public List<string> GetStringsPost()
        {
            return new List<string> { "value1", "value2" };
        }
    }
}
