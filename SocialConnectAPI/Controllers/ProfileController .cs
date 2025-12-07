using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.CommonConfig;
using SocialConnectAPI.BAL.IService;
using System.Security.Claims;

namespace SocialConnectAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid token"));

                var profile = await _profileService.GetProfileAsync(userId);

                return Ok(new Response{ Message = Messages.Successfully,Status = APIStatus.Successful,Data = profile});
            }
            catch (Exception ex)
            { 
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
