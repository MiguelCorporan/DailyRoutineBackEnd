//namespace DailyRoutine.Models
//{
//    public class Notification
//    {
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace DailyRoutine.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;

        public bool IsScheduled { get; set; } = false; 
    }
}
