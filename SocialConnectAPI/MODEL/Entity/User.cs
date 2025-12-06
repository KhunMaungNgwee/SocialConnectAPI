using MODEL.CommonConfig;
using SocialConnectAPI.MODEL.Entity;
using System.ComponentModel.DataAnnotations;

namespace MODEL.Entity
{
    public  class User : Common
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
