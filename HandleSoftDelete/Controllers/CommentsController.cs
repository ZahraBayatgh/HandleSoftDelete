using HandleSoftDelete.Dtos;
using HandleSoftDelete.Services;
using Microsoft.AspNetCore.Mvc;

namespace HandleSoftDelete.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/comments
        [HttpGet]
        public IActionResult GetComments([FromQuery] int postId, [FromQuery] bool includeDeleted = false)
        {
            var comments = _commentService.GetComments(postId, includeDeleted).ToList();
            return Ok(comments);
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var comment = _commentService.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // POST: api/comments
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest("Comment is null.");
            }

            await _commentService.AddCommentAsync(commentDto);
            return Ok();
        }

        // PUT: api/comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDto commentDto)
        {
            var existingComment = _commentService.GetComment(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            await _commentService.UpdateCommentAsync(existingComment.PostId, commentDto);
            return NoContent();
        }

        // DELETE: api/comments/soft/5
        [HttpDelete("soft/{id}")]
        public async Task<IActionResult> SoftDeleteComment(int id)
        {
            var comment = _commentService.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentService.SoftDeleteCommentAsync(id);
            return NoContent();
        }

        // DELETE: api/comments/hard/5
        [HttpDelete("hard/{id}")]
        public async Task<IActionResult> HardDeleteComment(int id)
        {
            var comment = _commentService.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentService.HardDeleteCommentAsync(id);
            return NoContent();
        }
    }
}