using Microsoft.AspNetCore.Mvc;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet] //http://localhost:[port]/api/Values
        public List<string> Get()
        {
            return new List<string>() { "Bojan", "Toshe", "Kostadin" };
        }

        [HttpGet("info")] //http://localhost:[port]/api/Values/info
        public string GetInfo()
        {
            return "This is some info for our response.";
        }

        //This can not be accessed, because the URL is the same for some other endpoint. So there will be an API Error
        //[HttpGet]
        //public string GetString()
        //{
        //    return "Some text.";
        //}

        [HttpPost]
        public string Post()
        {
            return "Ok";
        }

        [HttpGet("details/{id}")]
        public int GetById(int id)
        {
            return id;
        }
    }
}
