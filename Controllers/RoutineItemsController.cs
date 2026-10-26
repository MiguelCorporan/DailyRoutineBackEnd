//namespace DailyRoutine.Controllers
//{
//    public class RoutineItemsController
//    {
//    }
//}

using DailyRoutine.DTOs.RoutineItemsDTO;
using DailyRoutine.Interfaces;
using DailyRoutine.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DailyRoutine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoutineItemsController : ControllerBase
    {
        private readonly IRoutineService _service;

        public RoutineItemsController(IRoutineService service)
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
            var items = await _service.GetAllAsync(userId);

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoutineItemCreateDto dto)
        {
            var userId = GetUserId();
            var item = await _service.CreateAsync(dto, userId);

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoutineItemUpdateDto dto)
        {
            var userId = GetUserId();
            var updated = await _service.UpdateAsync(id, dto, userId);

            if (updated == null)
                return NotFound("Item no encontrado o no pertenece al usuario.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var deleted = await _service.DeleteAsync(id, userId);

            if (!deleted)
                return NotFound("Item no encontrado o no pertenece al usuario.");

            return Ok(new { message = "Item eliminado." });
        }
    }
}
