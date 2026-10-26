//namespace DailyRoutine.Interfaces
//{
//    public class IRoutineService
//    {
//    }
//}

using DailyRoutine.DTOs.RoutineItemsDTO;

namespace DailyRoutine.Interfaces
{
    public interface IRoutineService
    {
        //Task<RoutineItemResponseDto> CreateAsync(RoutineItemCreateDto dto, int userId);
        //Task<List<RoutineItemResponseDto>> GetAllAsync(int userId);
        //Task<RoutineItemResponseDto?> UpdateAsync(int id, RoutineItemUpdateDto dto, int userId);
        //Task<bool> DeleteAsync(int id, int userId);

        Task<RoutineItemResponseDto> CreateAsync(RoutineItemCreateDto dto, int userId);
        Task<IEnumerable<RoutineItemResponseDto>> GetAllAsync(int userId);
        Task<RoutineItemResponseDto?> UpdateAsync(int id, RoutineItemUpdateDto dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
