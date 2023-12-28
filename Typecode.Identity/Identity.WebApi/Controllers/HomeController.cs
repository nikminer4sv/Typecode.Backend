using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [HttpGet]
    [Route("info")]
    public IActionResult Info()
    {
        return Ok("From authorized method");
    }
}