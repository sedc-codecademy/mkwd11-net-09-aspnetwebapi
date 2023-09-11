using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using Serilog;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly IUsersService _usersService;
        public ExternalController(INotesService notesService, IUsersService usersService)
        {
            _notesService = notesService;
            _usersService = usersService;
        }

        [HttpGet("performance")]
        public ActionResult<long> GetNotesServicePerformance()
        {
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i<100; i++)
            {
                _notesService.GetNoteById(1);
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        [HttpGet("registerTestUser")]
        public IActionResult RegisterTestUser()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage
                        = httpClient.GetAsync("http://localhost:46583/api/testdata/testUser").Result;
                    string responseBody = httpResponseMessage.Content.ReadAsStringAsync().Result; //--> Json string

                    RegisterUserDto registerUserDto = JsonConvert.DeserializeObject<RegisterUserDto>(responseBody);

                    _usersService.Register(registerUserDto);

                }
                return Ok("User registered");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }
    }
}
