using MODEL;
using REPOSITORY;
using SocialConnectAPI.MODEL.Entity;
using SocialConnectAPI.REPOSITORY.IRepository;

namespace SocialConnectAPI.REPOSITORY.Repository
{
    public class ReactionRepository : GenericRepository<Reaction> , IReactionRepository
    {
        public ReactionRepository(DataContext context) : base(context) { }
    }
}
