using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]  //http://localhost:[port]/api/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet] //no additional route
        //http://localhost:[port]/api/values
        public List<string> Get()
        {
            return new List<string>() { "value1", "value2" };
        }


        [HttpGet("info")] //http://localhost:[port]/api/values/info
        public string GetInfo()
        {
            return "This is our notes app";
        }

        //HAS SAME HTTPMETHOD AND SAME ADDRESS!!! -> ERROR
        //[HttpGet]
        //public string GetString()
        //{
        //    return "test";
        //}


        //ALLOWED
        //THE HTTP METHOD IS DIFFERENT
        [HttpPost]
        public string Post()
        {
            return "Ok";
        }

        [HttpGet("details/{id}")] //http://localhost:[port]/api/values/details/1
        public int GetById(int id)
        {
            return id;
        }
    }
}
