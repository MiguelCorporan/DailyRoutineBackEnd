using System.ComponentModel.DataAnnotations;

namespace DailyRoutine.Models

{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User"; // por defecto "User"

        [Required]
        public string Birthdate { get; set; } = string.Empty;

        public List<RefreshTokenEntity> RefreshTokens { get; set; } = new();
    }
}
