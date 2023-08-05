using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OurFirstAppG5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("strings")]
        public IEnumerable<string> GetStrings()
        {
            return new List<string> { "value1", "value2" };
        }

        [HttpGet("string")]
        public string GetString()
        {
            return "value3";
        }

    }
}
