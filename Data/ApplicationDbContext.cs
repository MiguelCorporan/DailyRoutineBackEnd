using Microsoft.EntityFrameworkCore;
using DailyRoutine.Models;

namespace DailyRoutine.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public DbSet<RoutineItem> RoutineItems { get; set; }

        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        public DbSet<Notification> Notifications { get; set; }

    }
}
