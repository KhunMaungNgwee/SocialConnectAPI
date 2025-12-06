using MODEL.CommonConfig;
using SocialConnectAPI.REPOSITORY.IRepository;

namespace REPOSITORY.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository IUserRepo { get; }
        IPostRepository IPostRepo { get; }
        ICommentRepository ICommentRepo { get; }    
        IReactionRepository IReactionRepo { get; }
        AppSetting AppSetting {  get; }
        Task<int> SaveChangesAsync();
    }
}
