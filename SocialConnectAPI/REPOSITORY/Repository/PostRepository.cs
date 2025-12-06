using MODEL;
using REPOSITORY;
using SocialConnectAPI.MODEL.Entity;
using SocialConnectAPI.REPOSITORY.IRepository;

namespace SocialConnectAPI.REPOSITORY.Repository
{
    public class PostRepository : GenericRepository<Post> , IPostRepository
    {
        public PostRepository(DataContext context) : base(context) { }
    }
}
