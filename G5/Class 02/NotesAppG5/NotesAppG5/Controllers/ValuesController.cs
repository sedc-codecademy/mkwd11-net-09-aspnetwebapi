using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotesAppG5.Controllers
{
    [Route("api/[controller]")] //http://localhost:[port]/api/Values
    //it refers to all actions in the controller
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet] //no additional route
        //http://localhost:[port]/api/Values
        public List<string> Get()
        {
            return new List<string>() { "value1", "value2" };
        }

        [HttpGet("info")]
        //http://localhost:[port]/api/Values/info
        public string GetInfo()
        {
            return "This is our new notes app!";
        }

        //HAS SAME HTTP METHOD AND SAME ADDRESS -> ERROR
        //[HttpGet]
        ////http://localhost:[port]/api/Values
        //public string GetString()
        //{
        //    return "string1";
        //}

        [HttpPost]
        //http://localhost:[port]/api/Values
        public string Post()
        {
            return "Ok";
        }

        [HttpGet("details/{id}")]
        //http://localhost:[port]/api/Values/details/1
        public int GetId(int id)
        {
            return id;
        }

    }
}
