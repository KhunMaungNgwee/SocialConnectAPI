using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.CommonConfig
{
    public class Common
    {
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        
    }
}
