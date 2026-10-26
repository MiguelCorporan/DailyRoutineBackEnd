using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DailyRoutine.Models
{
    public class RefreshTokenEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public DateTime Created { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserEntity")]
        public int UserId { get; set; }

        public UserEntity User { get; set; } = null!;
    }
}
