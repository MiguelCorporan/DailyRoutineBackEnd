//namespace DailyRoutine.DTOs
//{
//    public class LoginResponseDto
//    {
//    }
//}

namespace DailyRoutine.DTOs.LoginDTO
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? RefreshToken { get; set; } // lo usaremos luego
    }
}
