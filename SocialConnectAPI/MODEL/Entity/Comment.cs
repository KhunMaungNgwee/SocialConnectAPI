using MODEL.CommonConfig;
using MODEL.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnectAPI.MODEL.Entity
{
    public class Comment : Common
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("post_id")]
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
