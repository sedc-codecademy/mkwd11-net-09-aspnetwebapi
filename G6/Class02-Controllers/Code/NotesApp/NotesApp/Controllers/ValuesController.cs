using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //we return data, so we use HTTP GET
        //to get the route for an action we combine Route attribute and the Http[Method] attribute
        [HttpGet] //http://localhost:[port]/api/values
        public List<string> Get() 
        {
            return new List<string>() { "c#", "api", ".net" };
        }

        [HttpGet("info")] //http://localhost:[port]/api/values/info
        public string GetString()
        {
            return "This is our notes app";
        }

        [HttpGet("info/{id}")] //http://localhost:[port]/api/values/info/1
        public string GetInfo(int id)
        {
            return "Go the gym";
        }

        //[HttpGet] //http://localhost:[port]/api/values ERROR -> TWO ENDPOINTS WITH SAME ADDRESS
        //public string GetValues()
        //{
        //    return "c#";
        //}

        //ALLOWED
        //we have two methods with same route, but different HTTP METHOD!!! 
        [HttpPost] //http://localhost:[port]/api/values
        public string Post()
        {
            return "Ok";
        }
    }
}
