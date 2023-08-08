using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")] //https://localhost:[port]/api/values, GET/POST
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            return new List<string> { "value1", "value2" };
        }

        //[HttpGet] => has same HTTP method throws an error
        //public string GetById(int id)
        //{
        //    return $"The result for element with id: {id}";
        //}

        [HttpGet("details/{id}")] //https://localhost:[port]/api/values/details/2, GET
        public string GetById(int id)
        {
            return $"The result for element with id: {id}";
        }

        //[HttpGet("details")]
        //public string GetById(int id)
        //{
        //    return $"The result for element with id: {id}";
        //}

        [HttpPost]
        public string Post()
        {
            return "Ok";
        }
    }
}
