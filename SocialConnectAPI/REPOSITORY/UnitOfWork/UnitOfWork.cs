using Microsoft.Extensions.Options;
using MODEL;
using MODEL.CommonConfig;
using SocialConnectAPI.REPOSITORY.IRepository;
using SocialConnectAPI.REPOSITORY.Repository;

namespace REPOSITORY.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork

    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context, IOptions<AppSetting> appSetting)
        {
            _context = context;
            IUserRepo = new UserRepository(_context);
            IPostRepo = new PostRepository(_context);
            IReactionRepo = new ReactionRepository(_context);
            ICommentRepo = new CommentRepository(_context);

            AppSetting = appSetting.Value;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public IUserRepository IUserRepo { get; set; }
        public IPostRepository IPostRepo { get; set; }
        public IReactionRepository IReactionRepo { get; set; }
        public ICommentRepository ICommentRepo { get; set; }
       
        public AppSetting AppSetting { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
