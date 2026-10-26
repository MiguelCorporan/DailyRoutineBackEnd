namespace DailyRoutine.Interfaces
{
    public interface INotificationService
    {
        Task CreateAsync(int userId, string message, bool scheduled = false);
    }
}
