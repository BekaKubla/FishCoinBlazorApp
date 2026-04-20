using FishCoinBlazorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FishCoinBlazorApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly SmsService _smsService;
        public NotificationController(SmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet]
        public async Task<IActionResult> SendBuySms([FromQuery] string phoneNumber, decimal totalAmount, decimal points)
        {
            await _smsService.SendBuySms(phoneNumber, totalAmount, points);
            return Ok();
        }
    }
}
