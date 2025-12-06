namespace SocialConnectAPI.MODEL.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public int PostCount { get; set; }
        public int CommentCount { get; set; }
        public int ReactionCount { get; set; }
    }
}
