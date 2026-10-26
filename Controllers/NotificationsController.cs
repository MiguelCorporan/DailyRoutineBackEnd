using DailyRoutine.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyRoutine.Controllers
{
    //public class NotificationsController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationQueryService _service;

        public NotificationsController(INotificationQueryService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var result = await _service.GetAllAsync(userId);
            return Ok(result);
        }
    }

}
