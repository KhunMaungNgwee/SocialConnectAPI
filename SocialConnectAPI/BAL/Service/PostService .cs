using REPOSITORY.UnitOfWork;
using SocialConnectAPI.BAL.IService;
using SocialConnectAPI.MODEL.DTO;
using SocialConnectAPI.MODEL.Entity;

namespace SocialConnectAPI.BAL.Service
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public async Task<Post> CreatePostAsync(int userId, CreatePostDTO dto)
        {
            var post = new Post {
                UserId = userId,
                Title = dto.Title,
                Content = dto.Content,
                Image = dto.Image,
                CreatedAt = DateTime.UtcNow 
            };
            await _unitOfWork.IPostRepo.Add(post);
            await _unitOfWork.SaveChangesAsync();
            return post;
        }
        public async Task<Post> EditPostAsync(int userId, int postId, CreatePostDTO dto)
        {
            var post = (await _unitOfWork.IPostRepo.GetByCondition(p => p.Id == postId)).FirstOrDefault();
            if (post == null) throw new Exception("Post not found");
            if (post.UserId != userId) throw new Exception("Unauthorized to edit this post");

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.Image = dto.Image;
            await _unitOfWork.SaveChangesAsync();
            return post;
        }

        public async Task<IEnumerable<Post>> GetMyPostsAsync(int userId, int page, int pageSize)
        {
            var posts = (await _unitOfWork.IPostRepo.GetByCondition(p => p.UserId == userId))
                        .OrderByDescending(p => p.CreatedAt)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);
            return posts;
        }
    }

}
