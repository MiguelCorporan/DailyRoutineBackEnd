//namespace DailyRoutine.DTOs
//{
//    public class LoginRequestDto
//    {
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace DailyRoutine.DTOs.LoginDTO
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
