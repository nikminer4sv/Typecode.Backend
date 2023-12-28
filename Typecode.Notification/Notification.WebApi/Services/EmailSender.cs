using System.Net;
using System.Net.Mail;
using Notification.WebApi.Interfaces;
using Notification.WebApi.Models;

namespace Notification.WebApi.Services;

public class EmailSender : BaseSender<EmailSenderConfiguration>, IEmailNotificationSender, IDisposable
{
    private readonly SmtpClient _smtpClient;
    private readonly MailAddress _sender;
    
    public EmailSender(IConfiguration configuration) : base("EmailSenderConfiguration", configuration)
    {
        _smtpClient = new SmtpClient();
        _smtpClient.Host = SenderConfiguration.Host;
        _smtpClient.Port = SenderConfiguration.Port;
        _smtpClient.EnableSsl = SenderConfiguration.EnableSSL;
        
        var credentials = new NetworkCredential();
        credentials.UserName = SenderConfiguration.Username;
        credentials.Password = SenderConfiguration.Password;
        _smtpClient.Credentials = credentials;
        
        _sender = new MailAddress(SenderConfiguration.Username, SenderConfiguration.Username);
    }
    
    public override async Task Send(BaseNotificationRequest notificationRequest)
    {
        var request = (EmailNotificationRequest) notificationRequest;
        MailAddress receiver = new MailAddress(request.Email, request.Email);
        MailMessage message = new MailMessage(_sender, receiver);
        message.Subject = request.Subject;
        message.Body = request.Message;
        await _smtpClient.SendMailAsync(message);
    }

    public void Dispose()
    {
        _smtpClient.Dispose();
    }
}