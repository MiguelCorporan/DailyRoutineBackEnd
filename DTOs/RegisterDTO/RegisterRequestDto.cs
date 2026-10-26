//namespace DailyRoutine.DTOs
//{
//    public class RegisterRequestDto
//    {
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace DailyRoutine.DTOs.RegisterDTO
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // por defecto

        [Required]
        public string Birthdate { get; set; } = string.Empty;
    }
}

