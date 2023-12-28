using Microsoft.AspNetCore.Mvc;
using Notification.WebApi.Interfaces;
using Notification.WebApi.Models;

namespace Notification.WebApi.Controllers;

public class BaseNotificationController<T> : Controller where T : BaseNotificationRequest
{
    protected readonly INotificationSender _sender;

    protected BaseNotificationController(INotificationSender sender) => _sender = sender;
}