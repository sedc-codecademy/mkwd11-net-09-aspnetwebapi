using Books.Api.Data;
using Books.Api.Dtos;
using Books.Api.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BooksDbContext dbContext;

        public AuthorController(BooksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var result = dbContext.Authors.Select(x => new AuthorDto
            {
                Id = x.Id,
                Name = x.Name,
                DateOfBirth = x.BirthYear
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateAuthor(CreateAuthorDto dto)
        {
            var author = new Author
            {
                BirthYear = dto.DateOfBirth,
                Name = dto.Name
            };
            dbContext.Authors.Add(author);
            dbContext.SaveChanges();
            return Ok(author);
        }
    }
}
