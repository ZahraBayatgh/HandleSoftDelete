using HandleSoftDelete.Dtos;
using HandleSoftDelete.Entities;
using HandleSoftDelete.Repositories;
using HandleSoftDelete.Services;
using Microsoft.AspNetCore.Mvc;

namespace HandleSoftDelete.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts
        [HttpGet]
        public IActionResult GetPosts([FromQuery] bool includeDeleted = false)
        {
            var posts = _postService.GetPosts(includeDeleted).ToList();
            return Ok(posts);
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDto post)
        {
            if (post == null)
            {
                return BadRequest("Post is null.");
            }

           await _postService.AddPostAsync(post);
            return Ok();
        }

        // PUT: api/posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDto post)
        {
            if (post == null)
            {
                return BadRequest("Post is null or ID mismatch.");
            }

            var existingPost = _postService.GetPost(id);
            if (existingPost == null)
            {
                return NotFound();
            }

          await  _postService.UpdatePost(post);
            return NoContent();
        }

        // DELETE: api/posts/soft/5
        [HttpDelete("soft/{id}")]
        public async Task< IActionResult> SoftDeletePost(int id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

           await _postService.SoftDeletePost(id);
            return NoContent();
        }

        // DELETE: api/posts/hard/5
        [HttpDelete("hard/{id}")]
        public async Task<IActionResult> HardDeletePost(int id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }

           await _postService.HardDeletePost(id);
            return NoContent();
        }
    }
}
