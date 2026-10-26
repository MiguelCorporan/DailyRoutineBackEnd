//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace DailyRoutine.Models
//{
//    public class RoutineItem
//    {
//    }
//}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutine.Models
{
    public class RoutineItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        // 🔐 Relación con el usuario
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; }
    }
}
