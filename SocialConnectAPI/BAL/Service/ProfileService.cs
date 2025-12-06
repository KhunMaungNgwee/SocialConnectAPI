using REPOSITORY.UnitOfWork;
using SocialConnectAPI.BAL.IService;
using SocialConnectAPI.MODEL.DTO;

namespace SocialConnectAPI.BAL.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfileDTO> GetProfileAsync(int userId)
        {
            var user = (await _unitOfWork.IUserRepo.GetByCondition(u => u.Id == userId)).FirstOrDefault();

            if (user == null)
                throw new Exception("User not found");

            var postCount = (await _unitOfWork.IPostRepo.GetByCondition(p => p.UserId == userId)).Count();

            var commentCount = (await _unitOfWork.ICommentRepo.GetByCondition(c => c.UserId == userId)).Count();

            var reactionCount = (await _unitOfWork.IReactionRepo.GetByCondition(r => r.UserId == userId)).Count();

            return new UserProfileDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                PostCount = postCount,
                CommentCount = commentCount,
                ReactionCount = reactionCount
            };
        }
    }
}
