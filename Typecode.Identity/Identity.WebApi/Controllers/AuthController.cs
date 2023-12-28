using System.Security.Claims;
using Identity.WebApi.Interfaces;
using IdentityModel.Client;
using Identity.WebApi.Models;
using IdentityServer.WebApi.ViewModels;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly HttpClient _httpClient;
    private readonly IProfileService _profileService;

    public AuthController(UserManager<User> userManager, HttpClient httpClient, IProfileService profileService)
    {
        _userManager = userManager;
        _httpClient = httpClient;
        _profileService = profileService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
    {
        var identityServerResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = "http://localhost:5171/connect/token",
            GrantType = "password",

            ClientId = "typecode-api",
            ClientSecret = "a75a559d-1dab-4c65-9bc0-f8e590cb388d",
            Scope = "TypecodeApi",
            UserName = loginViewModel.Username,
            Password = loginViewModel.Password,
        });

        if (!identityServerResponse.IsError)
            return Ok(new AuthenticateResponseViewModel() {AccessToken = identityServerResponse.AccessToken});
        
        return BadRequest("Invalid username or password");
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
    {
        var user = new User
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email,
            DateOfBirth = DateTime.Now
        };
        var result = await _userManager.CreateAsync(user, registerViewModel.Password);
        if (result.Succeeded)
            return Ok(new RegisterResponseViewModel {Username = user.UserName, Password = registerViewModel.Password});
        return BadRequest("Invalid data");
    }
    
    [HttpGet]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("profile")]
    public Profile GetProfile()
    {
        
        IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity!).Claims;
        var email = claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
        return _profileService.GetProfile(email);
    }
    
    [HttpGet]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("id")]
    public string GetUserId()
    {
        IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity!).Claims;
        var id = claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
        return id;
    }
    
    [HttpGet]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("email")]
    public string GetUserEmail()
    {
        IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity!).Claims;
        var email = claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
        return email;
    }
    
    [HttpGet]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("role")]
    public string GetUserRole()
    {
        IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity!).Claims;
        var role = claims.FirstOrDefault(claim => claim.Type == "role")?.Value;
        return role;
    }
}