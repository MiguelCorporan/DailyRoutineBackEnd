//namespace DailyRoutine.DTOs
//{
//    public class RegisterResponseDto
//    {
//    }
//}

namespace DailyRoutine.DTOs.RegisterDTO
{
    public class RegisterResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? UserId { get; set; }
    }
}
