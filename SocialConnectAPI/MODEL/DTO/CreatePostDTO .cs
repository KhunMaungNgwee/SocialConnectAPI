namespace SocialConnectAPI.MODEL.DTO
{
    public class CreatePostDTO 
    { 
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!; 
        public string? Image { get; set; } 
    }

}
