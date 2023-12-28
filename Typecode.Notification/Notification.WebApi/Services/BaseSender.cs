using Notification.WebApi.Interfaces;
using Notification.WebApi.Models;

namespace Notification.WebApi.Services;

public abstract class BaseSender<T> : INotificationSender where T: BaseSenderConfiguration
{
    protected readonly T SenderConfiguration;

    protected BaseSender(string sectionName, IConfiguration configuration)
    {
        SenderConfiguration = configuration.GetSection(sectionName).Get<T>() ?? throw new Exception("Configuration can't be reached.");
    }

    public abstract Task Send(BaseNotificationRequest notificationRequest);
}