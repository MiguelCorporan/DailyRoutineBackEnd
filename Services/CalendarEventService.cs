using System;
using DailyRoutine.Data;
using DailyRoutine.DTOs.CalenderDTO;
using DailyRoutine.Interfaces;
using DailyRoutine.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace DailyRoutine.Services
{
    public class CalendarEventService : ICalendarEventService
    {

        private readonly ApplicationDbContext _context;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly INotificationService _notif;

        public CalendarEventService(ApplicationDbContext context, IServiceScopeFactory scopeFactory, INotificationService notif)
        {
            _context = context;
            _scopeFactory = scopeFactory;
            _notif = notif; 
        }

        public async Task<IEnumerable<CalendarEventResponseDto>> GetAllAsync(int userId)
        {
            var itm = await _context.CalendarEvents
            .Where(e => e.UserId == userId)
            .ToListAsync();

           

            return itm.Select(e => new CalendarEventResponseDto
            {
                Id = e.Id,
                Title = e.Tiltle,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                CreatedAt = e.StartDate,
                UpdatedAt = DateTime.UtcNow,
            });

        }


        public async Task<CalendarEventResponseDto> GetByIdAsync(int Id, int userId)
        {
            var Aitm = await _context.CalendarEvents.FirstOrDefaultAsync(e => e.Id == Id);

            if (Aitm == null || Aitm.UserId != userId)
                return null;


            return new CalendarEventResponseDto
            {
                Id = Aitm.Id,
                Title = Aitm.Tiltle,
                Description = Aitm.Description,
                StartDate = Aitm.StartDate,
                EndDate = Aitm.EndDate,
                CreatedAt = Aitm.StartDate,
            };

        }

        public async Task<CalendarEventResponseDto> CreateAsync(CalendarEventCreateDto dto, int UserId)
        {
            var Nitm = new CalendarEvent
            {
                Tiltle = dto.Title,
                Description = dto.Desccription,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                UserId = UserId,

            };

             _context.CalendarEvents.Add(Nitm);
               await _context.SaveChangesAsync();


            await _notif.CreateAsync(UserId, $"Has creado el evento \"{Nitm.Tiltle}\" para el {Nitm.StartDate:d}");

            BackgroundJob.Schedule<HangfireJobService>(
                job => job.SendCalendarReminder(Nitm.Id),
                Nitm.StartDate
            );


            return new CalendarEventResponseDto
            {
                Id = Nitm.Id,
                Title = Nitm.Tiltle,
                Description = Nitm.Description,
                StartDate = Nitm.StartDate,
                EndDate = Nitm.EndDate,
                CreatedAt = Nitm.StartDate,
            };
        }


        public async Task<CalendarEventResponseDto> UpdateAsync(int userId, CalendarEventUpdateDto Dto, int Id) 
        {
            var ev = await _context.CalendarEvents.FirstOrDefaultAsync
                (e => e.Id == Id);

            if (ev == null || ev.UserId != userId) return null;

            ev.Tiltle = Dto.title;
            ev.Description = Dto.description;
            ev.StartDate = Dto.StartTime;
            ev.EndDate = Dto.EndTime;

            await _context.SaveChangesAsync();


            return new CalendarEventResponseDto
            {
                Id = ev.Id,
                Title = ev.Tiltle,
                Description = ev.Description,
                StartDate = ev.StartDate,
                EndDate = ev.EndDate,
                CreatedAt = ev.StartDate
            };
        }

        public async Task<bool> DeleteAsync(int Id, int userId)
        {
            var Ditm = await _context.CalendarEvents.FirstOrDefaultAsync (e => e.Id == Id);

            if (Ditm == null || Ditm.UserId != userId) return false;

            _context.CalendarEvents.Remove(Ditm);
                _context.SaveChanges();

            return true;
        }
    }
}
