using SocialConnectAPI.MODEL.DTO;
using SocialConnectAPI.MODEL.Entity;

namespace SocialConnectAPI.BAL.IService
{
    public interface INewsfeedService
    {
        Task<IEnumerable<PostResponseDTO>> GetAllPostsAsync(int page, int pageSize);
        Task<Comment> CommentOnPostAsync(int userId, int postId, CreateCommentDTO dto);
        Task<ReactionResponseDTO> ToggleReactionAsync(int userId, int postId, string type);
    }

}
