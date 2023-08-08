using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/v1/Values
        public ValuesController()
        {

        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/v1/Values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/v1/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/v1/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //  DELETE api/v1/Values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
