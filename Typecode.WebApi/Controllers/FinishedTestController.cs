using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typecode.Application.Interfaces;
using Typecode.Application.ViewModels.FinishedTestViewModel;
using Typecode.Application.ViewModels.TestViewModel;
using Typecode.Domain;
using Typecode.WebApi.ViewModels;

namespace Typecode.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FinishedTestController : BaseController
{
    private readonly IFinishedTestService _finishedTestService;
    private readonly ITestService _testService;
    
    public FinishedTestController(HttpClient httpClient, IFinishedTestService finishedTestService, ITestService testService) : base(httpClient)
    {
        _finishedTestService = finishedTestService;
        _testService = testService;
    }

    [HttpGet]
    public async Task<IEnumerable<FinishedTestDto>> GetAllByUserId()
    {
        HttpResponseMessage response;
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://auth:5171/api/auth/id"))
        {
            requestMessage.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());
    
            response = await _httpClient.SendAsync(requestMessage);
        }
        response.EnsureSuccessStatusCode();

        var id = Guid.Parse(await response.Content.ReadAsStringAsync());

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
                
                var model = new FinishedTestDto
                {
                    Title = finishedTest.Title,
                    Accuracy = test.Accuracy,
                    Time = test.Time
                };
                finishedTests.Add(model);
            }

            return finishedTests;
        }

        return new List<FinishedTestDto>();
    }
    
    [HttpPost]
    public async Task Add([FromBody] FinishedTestViewModel finishedTestViewModel)
    {
        HttpResponseMessage response;
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://auth:5171/api/auth/id"))
        {
            requestMessage.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());
    
            response = await _httpClient.SendAsync(requestMessage);
        }
        response.EnsureSuccessStatusCode();

        var id = Guid.Parse(await response.Content.ReadAsStringAsync());
        finishedTestViewModel.UserId = id;
        await _finishedTestService.AddAsync(finishedTestViewModel);
    }
}