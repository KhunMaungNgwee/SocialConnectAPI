using MODEL;
using REPOSITORY;
using SocialConnectAPI.MODEL.Entity;
using SocialConnectAPI.REPOSITORY.IRepository;

namespace SocialConnectAPI.REPOSITORY.Repository
{
    public class CommentRepository : GenericRepository<Comment> , ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context) { }
    }
}
