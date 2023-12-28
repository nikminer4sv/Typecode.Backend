using Microsoft.AspNetCore.Mvc;

namespace Typecode.WebApi.Controllers;

[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly HttpClient _httpClient;

    public BaseController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}