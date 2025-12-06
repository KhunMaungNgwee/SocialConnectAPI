using Microsoft.EntityFrameworkCore;
using MODEL.Entity;
using SocialConnectAPI.MODEL.Entity;

namespace MODEL
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }

}
