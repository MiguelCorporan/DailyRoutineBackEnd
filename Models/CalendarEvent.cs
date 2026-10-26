using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DailyRoutine.Models
{
    public class CalendarEvent
    {

        public int Id { get; set; }

        [Required]
        public string Tiltle { get; set; } 
        [Required]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; }


    }
}
