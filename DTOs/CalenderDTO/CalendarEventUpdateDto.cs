using System.ComponentModel.DataAnnotations;

namespace DailyRoutine.DTOs.CalenderDTO
{
    public class CalendarEventUpdateDto
    {

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
