using REPOSITORY.UnitOfWork;
using SocialConnectAPI.BAL.IService;
using SocialConnectAPI.MODEL.DTO;
using SocialConnectAPI.MODEL.Entity;

namespace SocialConnectAPI.BAL.Service
{
    public class NewsfeedService : INewsfeedService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsfeedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public async Task<IEnumerable<PostResponseDTO>> GetAllPostsAsync(int page, int pageSize)
        {
            var posts = (await _unitOfWork.IPostRepo.GetAll())
                        .OrderByDescending(p => p.CreatedAt)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);

            var result = new List<PostResponseDTO>();
            foreach (var post in posts)
            {
                var author = (await _unitOfWork.IUserRepo.GetByCondition(u => u.Id == post.UserId)).FirstOrDefault();
                var commentCount = (await _unitOfWork.ICommentRepo.GetByCondition(c => c.PostId == post.Id)).Count();
                var reactionCount = (await _unitOfWork.IReactionRepo.GetByCondition(r => r.PostId == post.Id)).Count();

                result.Add(new PostResponseDTO
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    Image = post.Image,
                    CreatedAt = post.CreatedAt,
                    Author = new AuthorDTO { Id = author!.Id, Name = author.Name },
                    CommentCount = commentCount,
                    ReactionCount = reactionCount
                });
            }
            return result;
        }

        public async Task<Comment> CommentOnPostAsync(int userId, int postId, CreateCommentDTO dto)
        {
            var post = (await _unitOfWork.IPostRepo.GetByCondition(p => p.Id == postId)).FirstOrDefault();
            if (post == null) throw new Exception("Post not found");

            var comment = new Comment
            {
                PostId = postId,
                UserId = userId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.ICommentRepo.Add(comment);
            await _unitOfWork.SaveChangesAsync();
            return comment;
        }

        public async Task<ReactionResponseDTO> ToggleReactionAsync(int userId, int postId, string type)
        {
            var reaction = (await _unitOfWork.IReactionRepo.GetByCondition(r => r.PostId == postId && r.UserId == userId)).FirstOrDefault();
            bool liked;
            if (reaction != null)
            {
                _unitOfWork.IReactionRepo.Delete(reaction);
                await _unitOfWork.SaveChangesAsync();
                liked = false;
            }
            else
            {
                var newReaction = new Reaction
                {
                    PostId = postId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };
                await _unitOfWork.IReactionRepo.Add(newReaction);
                await _unitOfWork.SaveChangesAsync();
                liked = true;
            }

            var count = (await _unitOfWork.IReactionRepo.GetByCondition(r => r.PostId == postId)).Count();

            return new ReactionResponseDTO
            {
                Liked = liked,
                Count = count
            };
        }
    }
}
