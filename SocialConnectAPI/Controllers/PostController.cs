using Microsoft.AspNetCore.Mvc;
using MODEL.CommonConfig;
using SocialConnectAPI.BAL.IService;
using SocialConnectAPI.MODEL.DTO;
using System.Security.Claims;

namespace SocialConnectAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("posts")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid token"));
                var post = await _postService.CreatePostAsync(userId, dto);
                return Ok(new Response { Message = Messages.Successfully, Status = APIStatus.Successful, Data = post });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }
        [HttpPut("posts/{postId}")]
        public async Task<IActionResult> EditPost(int postId, [FromBody] CreatePostDTO dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid token"));
                var post = await _postService.EditPostAsync(userId, postId, dto);
                return Ok(new Response { Message = Messages.Successfully, Status = APIStatus.Successful, Data = post });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }
        [HttpGet("my-posts")]
        public async Task<IActionResult> GetMyPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var userId = int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid token")); 
                var posts = await _postService.GetMyPostsAsync(userId, page, pageSize);
                return Ok(new Response { Message = Messages.Successfully, Status = APIStatus.Successful, Data = posts });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
