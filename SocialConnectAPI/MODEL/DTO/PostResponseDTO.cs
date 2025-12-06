namespace SocialConnectAPI.MODEL.DTO
{
    public class PostResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public AuthorDTO Author { get; set; } = default!;
        public int CommentCount { get; set; }
        public int ReactionCount { get; set; }
    }

    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }

    public class CreateCommentDTO
    {
        public string Content { get; set; } = default!;
    }

    public class ReactionDTO
    {
        public string Type { get; set; } = default!; 
    }

}
