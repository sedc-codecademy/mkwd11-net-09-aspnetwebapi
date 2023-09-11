using Microsoft.AspNetCore.Mvc;
using Notes.Services.Models;
using Notes.Services.Service.External;

namespace Notes.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlaceholderController : ControllerBase
    {
        private readonly IPlaceholderService placeholderService;

        public PlaceholderController(IPlaceholderService placeholderService)
        {
            this.placeholderService = placeholderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await placeholderService.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostModel model)
        {
            var result = await placeholderService.CreatePost(model);
            return Ok(result);
        }
    }
}
