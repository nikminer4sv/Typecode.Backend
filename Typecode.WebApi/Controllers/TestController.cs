using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typecode.Application.Interfaces;
using Typecode.Application.ViewModels.TestViewModel;
using Typecode.Domain;

namespace Typecode.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{
    private readonly ITestService _testService;
    
    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public async Task<IEnumerable<Test>> GetAll()
    {
        return await _testService.GetAllAsync();
    }
    
    [HttpDelete]
    public async Task DeleteAsync([FromQuery] Guid id)
    {
        await _testService.DeleteAsync(id);
    }
    
    [HttpPost]
    public async Task<Guid> Add([FromBody] TestViewModel testViewModel)
    {
        return await _testService.AddAsync(testViewModel);
    }
    
    [HttpPut]
    public async Task Update([FromQuery] Guid id, [FromBody] TestViewModel testViewModel)
    {
        await _testService.UpdateAsync(id, testViewModel);
    }

}