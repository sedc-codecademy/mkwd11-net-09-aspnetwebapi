using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.BLL.Models;
using Profiles.BLL.Services;

namespace Profiles.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfilesController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public IActionResult GetProfiles()
        {
            return Ok(profileService.GetProfiles());
        }

        [HttpPost]
        public IActionResult CreateProfile(CreateProfileModel model)
        {
            var profile = profileService.CreateProfile(model);
            return Created($"api/profiles/{profile.Id}", profile);
        }
        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            return Ok(profileService.GetProfile(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProfileModel model)
        {
            return Ok(profileService.Update(id, model));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(profileService.Delete(id));
        }

        [HttpPost("connect-to/{id}")]
        public IActionResult ConnectTo(int id)
        {
            var currentId = 1;
            profileService.Connect(currentId, id);
            return Ok();
        }
    }
}
