//namespace DailyRoutine.DTOs
//{
//    public class RoutineItemUpdateDto
//    {
//    }
//}

namespace DailyRoutine.DTOs.RoutineItemsDTO
{
    public class RoutineItemUpdateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
