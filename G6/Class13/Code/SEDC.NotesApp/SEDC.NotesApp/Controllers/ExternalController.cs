using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEDC.NotesApp.Dtos.Users;
using System.Text;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        [HttpGet]
        public ActionResult<UserDto> GetTestUser()
        {
            //ping TestAPI

            //get test user

            //return test user

            // !!both API-s must be alive, we must run them!!

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    //ping the GET endpoint from Test API
                    HttpResponseMessage response =
                          httpClient.GetAsync("http://localhost:5207/api/Test/testUser").Result;

                    //we need to get the content of the response
                    //the content is in JSON format, that means it is a JSON string
                    string content = response.Content.ReadAsStringAsync().Result;

                    UserDto user = JsonConvert.DeserializeObject<UserDto>(content);
                    return Ok(user);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult PostTestUser(UserDto user)
        {
            try
            {
                using(HttpClient httpClient = new HttpClient())
                {
                    string jsonContent = JsonConvert.SerializeObject(user);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response =
                    httpClient.PostAsync("http://localhost:5207/api/Test/addTestUser", content).Result;

                    return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
