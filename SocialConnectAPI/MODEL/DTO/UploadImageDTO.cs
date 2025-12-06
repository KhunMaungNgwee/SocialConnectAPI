namespace SocialConnectAPI.MODEL.DTO
{
    public class UploadImageDTO
    {
        public IFormFile ImageFile { get; set; }
        public string? Description { get; set; }
    }
}
