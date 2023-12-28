namespace Notification.WebApi.Models;

public class EmailNotificationRequest : BaseNotificationRequest
{
    public string Email { get; set; }
    public string Subject { get; set; }
}