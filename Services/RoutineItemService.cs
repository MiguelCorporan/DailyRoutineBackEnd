//namespace DailyRoutine.Services
//{
//    public class RoutineItemService
//    {
//    }
//}

using DailyRoutine.Data;
using DailyRoutine.DTOs.RoutineItemsDTO;
using DailyRoutine.Interfaces;
using DailyRoutine.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyRoutine.Services
{
    public class RoutineItemService : IRoutineService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notif;

        public RoutineItemService(ApplicationDbContext context, INotificationService notif)
        {
            _context = context;
            _notif = notif;
        }

        public async Task<RoutineItemResponseDto> CreateAsync(RoutineItemCreateDto dto, int userId)
        {
            var item = new RoutineItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            };

            _context.RoutineItems.Add(item);
            await _context.SaveChangesAsync();

            await _notif.CreateAsync(userId, $"Has creado la rutina \"{item.Title}\"");


            return new RoutineItemResponseDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted
            };
        }

        public async Task<IEnumerable<RoutineItemResponseDto>> GetAllAsync(int userId)
        {
            return await _context.RoutineItems
                .Where(x => x.UserId == userId)
                .Select(x => new RoutineItemResponseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsCompleted = x.IsCompleted
                })
                .ToListAsync();
        }

        public async Task<RoutineItemResponseDto?> UpdateAsync(int id, RoutineItemUpdateDto dto, int userId)
        {
            var item = await _context.RoutineItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (item == null)
                return null;

            item.Title = dto.Title;
            item.Description = dto.Description;
            item.IsCompleted = dto.IsCompleted;

            await _context.SaveChangesAsync();

            if (dto.IsCompleted)
            {
                await _notif.CreateAsync(userId, $"Completaste la rutina \"{item.Title}\" 🎉");
            }

            return new RoutineItemResponseDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted
            };
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var item = await _context.RoutineItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (item == null)
                return false;

            _context.RoutineItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
