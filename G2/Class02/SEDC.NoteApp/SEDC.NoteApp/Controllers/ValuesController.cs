using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SEDC.NoteApp.Controllers
{
    [Route("api/[controller]")] //https://localhost:[port]/api/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet] //https://localhost:[port]/api/values GET
        public IActionResult Get() 
        {
            var response = new List<string> { "Viktor", "Ilija" };
            return Ok(response);
            //return StatusCode(StatusCodes.Status200OK, response);
        }

        //[HttpGet] //We cant add another get moethod without specific route name
        //public IActionResult GetTwo()
        //{
        //    return Ok();
        //}

        [HttpGet("GetInfo")] //https://localhost:[port]/api/values/getinfo
        //[Route("GetInfo")]
        public IActionResult GetInfo() 
        {
            return Ok();
        }

        [HttpPost] //https://localhost:[port]/api/values POST
        public IActionResult Post() 
        {
            return Ok();
        }

        [HttpDelete] //https://localhost:[port]/api/values DELETE
        public IActionResult Delete()
        {
            return Ok();
        }

        [HttpPut] //https://localhost:[port]/api/values PUT
        public IActionResult Edit()
        {
            return Ok();
        }

        [HttpGet("error")] //https://localhost:[port]/api/values/error GET return status code 500 (internal server error)
        public IActionResult SimulateError() 
        {
            throw new Exception("something went wrong");
        }

    }
}
