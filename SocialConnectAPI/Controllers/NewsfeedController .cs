using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.CommonConfig;
using SocialConnectAPI.BAL.IService;
using SocialConnectAPI.MODEL.DTO;
using System.Security.Claims;

namespace SocialConnectAPI.Controllers
{
  
    [ApiController]
    [Route("api")]
    public class NewsfeedController : ControllerBase
    {
        private readonly INewsfeedService _newsfeedService;
        public NewsfeedController(INewsfeedService newsfeedService) 
        { 
            _newsfeedService = newsfeedService; 
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var posts = await _newsfeedService.GetAllPostsAsync(page, pageSize);
                return Ok(new Response { Message = Messages.Successfully, Status = APIStatus.Successful, Data = posts });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("posts/{postId}/comments")]
        public async Task<IActionResult> CommentOnPost(int postId, [FromBody] CreateCommentDTO dto)
        {
            try
            {
                var userId = int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid token"));
                var comment = await _newsfeedService.CommentOnPostAsync(userId, postId, dto);
                return Ok(new Response { Message = Messages.Successfully, Status = APIStatus.Successful, Data = comment });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }


        [Authorize]
        [HttpPost("posts/{postId}/reaction")]
        public async Task<IActionResult> ToggleReaction(int postId, [FromBody] ReactionRequestDTO dto)
        {
            try
            {
                var userId = int.Parse(  User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid token")); 
                var result = await _newsfeedService.ToggleReactionAsync(userId, postId, dto.Type);
                return Ok(new Response { Message = "Successfully", Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }


    }

}
