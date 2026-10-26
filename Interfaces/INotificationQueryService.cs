using DailyRoutine.Models;

namespace DailyRoutine.Interfaces
{
    public interface INotificationQueryService
    {
        Task<IEnumerable<Notification>> GetAllAsync(int userId);
    }
}
