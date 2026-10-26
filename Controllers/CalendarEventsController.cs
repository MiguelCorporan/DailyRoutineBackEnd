using DailyRoutine.DTOs.CalenderDTO;
using DailyRoutine.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DailyRoutine.Controllers

{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CalendarEventsController : ControllerBase
    {
        private readonly ICalendarEventService _service;

        public CalendarEventsController(ICalendarEventService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var events = await _service.GetAllAsync(userId);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();
            var ev = await _service.GetByIdAsync(id, userId);

            if (ev == null)
                return NotFound("Evento no encontrado o no pertenece al usuario.");

            return Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CalendarEventCreateDto dto)
        {
            var userId = GetUserId();
            var ev = await _service.CreateAsync(dto, userId);

            return Ok(ev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CalendarEventUpdateDto dto)
        {
            var userId = GetUserId();
            var updated = await _service.UpdateAsync(id, dto, userId);

            if (updated == null)
                return NotFound("Evento no encontrado o no pertenece al usuario.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var deleted = await _service.DeleteAsync(id, userId);

            if (!deleted)
                return NotFound("Evento no encontrado o no pertenece al usuario.");

            return Ok(new { message = "Evento eliminado." });
        }
    }

}