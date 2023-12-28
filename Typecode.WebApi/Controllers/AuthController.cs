using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typecode.Application.ViewModels.LoginViewModel;
using Typecode.Application.ViewModels.RegisterViewModel;

namespace Typecode.WebApi.Controllers;

public class AuthController : BaseController
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(HttpClient httpClient, ILogger<AuthController> logger) : base(httpClient)
    {
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginViewModel model)
    {
        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("http://auth:5171/api/auth/login", content);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            return BadRequest("Invalid login or password");
        }

        return Ok(await response.Content.ReadAsStringAsync());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel registerViewModel)
    {
        var content = new StringContent(JsonSerializer.Serialize(registerViewModel), Encoding.UTF8,
            System.Net.Mime.MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync("http://auth:5171/api/auth/register", content);
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadAsStringAsync());
    }
    
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        HttpResponseMessage response;
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://auth:5171/api/auth/profile"))
        {
            requestMessage.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());
    
            response = await _httpClient.SendAsync(requestMessage);
        }
        response.EnsureSuccessStatusCode();
        return Ok(await response.Content.ReadAsStringAsync());
    }
    
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [HttpGet("role")]
    public async Task<IActionResult> GetRole()
    {
        HttpResponseMessage response;
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://auth:5171/api/auth/role"))
        {
            requestMessage.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());
    
            response = await _httpClient.SendAsync(requestMessage);
        }
        response.EnsureSuccessStatusCode();
        var roleObj = new { Role = await response.Content.ReadAsStringAsync()};
        return Ok(roleObj);
    }
}