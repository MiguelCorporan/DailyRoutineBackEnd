using DailyRoutine.DTOs.CalenderDTO;

namespace DailyRoutine.Interfaces
{
    public interface ICalendarEventService
    {
        Task<IEnumerable<CalendarEventResponseDto>> GetAllAsync(int UserId);
        Task<CalendarEventResponseDto> GetByIdAsync(int Id, int UserId);
        Task<CalendarEventResponseDto> CreateAsync(CalendarEventCreateDto dto, int UserId);
        Task<CalendarEventResponseDto> UpdateAsync(int Id, CalendarEventUpdateDto dto, int UserId);
        Task<bool> DeleteAsync(int Id, int UserId);
    }
}
