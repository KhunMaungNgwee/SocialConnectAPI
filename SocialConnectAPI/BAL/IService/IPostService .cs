using SocialConnectAPI.MODEL.DTO;
using SocialConnectAPI.MODEL.Entity;

namespace SocialConnectAPI.BAL.IService
{
    public interface IPostService 
    { 
        Task<Post> CreatePostAsync(int userId, CreatePostDTO dto);
        Task<Post> EditPostAsync(int userId, int postId, CreatePostDTO dto);
        Task<IEnumerable<Post>> GetMyPostsAsync(int userId, int page, int pageSize);
    }

}
