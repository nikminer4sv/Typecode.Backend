using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typecode.Application.Interfaces;
using Typecode.WebApi.ViewModels;

namespace Typecode.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationController : BaseController
{

    private readonly IFinishedTestService _finishedTestService;
    private readonly ITestService _testService;

    public NotificationController(HttpClient httpClient, IFinishedTestService finishedTestService, ITestService testService) : base(httpClient)
    {
        _finishedTestService = finishedTestService;
        _testService = testService;
    }
    
    [HttpPost("send")]
    public async Task SendEmailReport()
    {
        HttpResponseMessage response;
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://auth:5171/api/auth/id"))
        {
            requestMessage.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());
    
            response = await _httpClient.SendAsync(requestMessage);
        }
        response.EnsureSuccessStatusCode();
        
        HttpResponseMessage userEmailResponse;
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://auth:5171/api/auth/email"))
        {
            requestMessage.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());
    
            userEmailResponse = await _httpClient.SendAsync(requestMessage);
        }
        userEmailResponse.EnsureSuccessStatusCode();

        var email = await userEmailResponse.Content.ReadAsStringAsync();

        var id = Guid.Parse(await response.Content.ReadAsStringAsync());
        var message = String.Empty;
        var userTests = await _finishedTestService.GetAllByUserIdAsync(id);
        var tests = await _testService.GetAllAsync();
        if (tests != null && tests.Count() > 0)
        {
            var finishedTests = new List<FinishedTestDto>();
            foreach (var test in userTests)
            {
                var finishedTest = tests.FirstOrDefault(x => x.Id == test.TestId);
                if (finishedTest == null)
                    continue;

                message +=
                    $"Test: {finishedTest.Title}\nAccuracy: {test.Accuracy}\nTime: {test.Time}\n--------------------------------\n";
                
                var model = new FinishedTestDto
                {
                    Title = finishedTest.Title,
                    Accuracy = test.Accuracy,
                    Time = test.Time
                };
                finishedTests.Add(model);
            }
        }

        var t = new EmailObject() {Message = message, Subject="Typecode Report", Email=email};
        var content = new StringContent(JsonSerializer.Serialize(t), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var emailResponse = await _httpClient.PostAsync("http://notification:5300/api/email/send", content);
        response.EnsureSuccessStatusCode();
    }
}

public class EmailObject {
    public string Message { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }
}