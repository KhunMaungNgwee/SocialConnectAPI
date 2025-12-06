using MODEL;
using MODEL.Entity;
using REPOSITORY;
using SocialConnectAPI.REPOSITORY.IRepository;

namespace SocialConnectAPI.REPOSITORY.Repository
{
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }
    }
}
