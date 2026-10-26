//namespace DailyRoutine.DTOs
//{
//    public class RoutineItemResponseDto
//    {
//    }
//}

namespace DailyRoutine.DTOs.RoutineItemsDTO
{
    public class RoutineItemResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
