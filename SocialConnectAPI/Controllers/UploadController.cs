using Microsoft.AspNetCore.Mvc;
using MODEL.CommonConfig;
using MODEL.Entity;
using SocialConnectAPI.BAL.Service;
using SocialConnectAPI.MODEL.DTO;
using System.Security.Claims;

namespace SocialConnectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IFileService _fileService;

        public UploadController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDTO dto)
        {
            try
            {
                // Get user ID from token (optional)
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (dto.ImageFile == null || dto.ImageFile.Length == 0)
                {
                    return Ok(new Response
                    {
                        Message = "No image file provided",
                        Status = APIStatus.Error
                    });
                }

                var imageUrl = await _fileService.UploadImageAsync(dto.ImageFile);

                return Ok(new Response
                {
                    Message = "Image uploaded successfully",
                    Status = APIStatus.Successful,
                    Data = new { ImageUrl = imageUrl }
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response
                {
                    Message = ex.Message,
                    Status = APIStatus.SystemError
                });
            }
        }

        [HttpDelete("image")]
        public async Task<IActionResult> DeleteImage([FromBody] DeleteImageDTO dto)
        {
            try
            {
                var result = await _fileService.DeleteImageAsync(dto.ImageUrl);

                return Ok(new Response
                {
                    Message = result ? "Image deleted successfully" : "Image not found",
                    Status = result ? APIStatus.Successful : APIStatus.Error
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response
                {
                    Message = ex.Message,
                    Status = APIStatus.SystemError
                });
            }
        }
    }

    public class DeleteImageDTO
    {
        public string ImageUrl { get; set; }
    }
}
