//namespace DailyRoutine.Services
//{
//    public class HangfireJobService
//    {
//    }
//}

using DailyRoutine.Data;
using DailyRoutine.Interfaces;
using DailyRoutine.Models;
using Microsoft.EntityFrameworkCore;

public class HangfireJobService
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notif;

    public HangfireJobService(ApplicationDbContext context, INotificationService notif)
    {
        _context = context;
        _notif = notif;
    }

    public async Task SendCalendarReminder(int eventId)
    {
        var ev = await _context.CalendarEvents.FirstOrDefaultAsync(e => e.Id == eventId);

        if (ev == null) return;

        await _notif.CreateAsync(ev.UserId, $"Hoy tienes el evento: \"{ev.Tiltle}\"");
    }
}
