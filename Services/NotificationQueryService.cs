//namespace DailyRoutine.Services
//{
//    public class NotificationQueryService
//    {
//    }
//}

using DailyRoutine.Data;
using DailyRoutine.Interfaces;
using DailyRoutine.Models;
using Microsoft.EntityFrameworkCore;

public class NotificationQueryService : INotificationQueryService
{
    private readonly ApplicationDbContext _context;

    public NotificationQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetAllAsync(int userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }
}
