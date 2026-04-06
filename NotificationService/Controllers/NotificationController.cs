using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendNotification([FromBody] NotificationModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Message))
            {
                return BadRequest("Message is required");
            }

            Console.WriteLine($"Notification: {model.Message}");

            return Ok(new
            {
                status = "Notification sent",
                message = model.Message
            });
        }

    }
}
