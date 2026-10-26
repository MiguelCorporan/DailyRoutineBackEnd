//namespace DailyRoutine.Interfaces
//{
//    public class IAuthService
//    {
//    }
//}

using DailyRoutine.DTOs.LoginDTO;
using DailyRoutine.DTOs.RegisterDTO;
using System.Threading.Tasks;

namespace DailyRoutine.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
