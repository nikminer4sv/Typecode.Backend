using Microsoft.AspNetCore.Mvc;
using Notification.WebApi.Interfaces;
using Notification.WebApi.Models;

namespace Notification.WebApi.Controllers;

[ApiController]
[Route("api/email")]
public class EmailNotificationController : BaseNotificationController<EmailNotificationRequest>
{
    
    public EmailNotificationController(IEmailNotificationSender sender) : base(sender) {}

    [HttpPost]
    [Route("send")]
    public async Task<IActionResult> Send([FromBody] EmailNotificationRequest request)
    {
        try
        {
            await _sender.Send(request);
            return Ok("Message was successfully sent.");
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}