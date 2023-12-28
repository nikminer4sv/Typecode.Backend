using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Typecode.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TempController : Controller
{
    [HttpGet]
    [Route("info")]
    public string Info()
    {
        return "Cool authorize";
    }
}