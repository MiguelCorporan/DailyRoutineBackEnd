using DailyRoutine.Data;
using DailyRoutine.Interfaces;
using DailyRoutine.Models;

namespace DailyRoutine.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {  
            _context = context; 
        }

        public async Task CreateAsync(int userId, string message, bool scheduled = false)
        {
            var notif = new Notification
            {
                UserId = userId,
                Message = message,
                IsScheduled = scheduled
            };

            _context.Notifications.Add(notif);
            await _context.SaveChangesAsync();
        }
    }

}
