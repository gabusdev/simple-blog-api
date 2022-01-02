using simple_blog_api.Models;
using simple_blog_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace simple_blog_api.Controllers
{
    [EnableCors("LocalReactPolicy")]
    [ApiController]
    [Route("blogs")]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;
        public BlogController(BlogService service)
        {
            _blogService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetAll() =>
            await _blogService.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Blog>> Get(string id)
        {
            var blog = await _blogService.GetAsync(id);

            if (blog is null)
                return NotFound();

            return blog;
        }

        [HttpPost]
        public async Task<ActionResult> Add(Blog blog)
        {
            await _blogService.AddAsync(blog);
            return CreatedAtAction(nameof(Add), new { id = blog.Id }, blog);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var blog = await _blogService.GetAsync(id);

            if (blog is null)
                return NotFound();

            await _blogService.DeleteAsync(id);

            return NoContent();
        }
    }
}