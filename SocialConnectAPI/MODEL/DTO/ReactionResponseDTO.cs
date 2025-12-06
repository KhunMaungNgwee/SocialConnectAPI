using System.ComponentModel.DataAnnotations;

namespace SocialConnectAPI.MODEL.DTO
{
    public class ReactionResponseDTO
    {
        public bool Liked { get; set; }
        public int Count { get; set; }
    }

    public class ReactionRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; } 
    }

}
