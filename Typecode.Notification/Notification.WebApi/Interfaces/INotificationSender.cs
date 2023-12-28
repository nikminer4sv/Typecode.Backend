using Notification.WebApi.Models;

namespace Notification.WebApi.Interfaces;

public interface INotificationSender
{ 
    Task Send(BaseNotificationRequest notificationRequest);
}