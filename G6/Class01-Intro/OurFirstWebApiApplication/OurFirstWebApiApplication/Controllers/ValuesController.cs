using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OurFirstWebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("getStrings")]
        public IEnumerable<string> GetStrings() //http://localhost:[port]/api/values/getStrings
        {
            return new List<string>() { "sedc", "api", "c#" };
        }

        [HttpGet("subjectName")]
        public string GetSubjectName()
        {
            return "Developing Web Apps using ASP.NET Core Web API";
        }

        [HttpGet("details/{id}")] //http://localhost:[port]/api/values/details/1
        public string GetDetails(int id)
        {
            return $"The id was: {id}";
        }
    }
}
