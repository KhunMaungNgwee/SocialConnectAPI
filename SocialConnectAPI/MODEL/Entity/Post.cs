using MODEL.CommonConfig;
using MODEL.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnectAPI.MODEL.Entity
{
    public class Post : Common
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
    }
}
