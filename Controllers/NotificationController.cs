using FMentorAPI.DTOs.RequestModel;
using FMentorAPI.Extensions.FCMNotification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FMentorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationRequestModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }
    }
}
