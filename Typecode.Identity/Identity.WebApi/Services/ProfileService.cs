using Identity.WebApi.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using IProfileService = Identity.WebApi.Interfaces.IProfileService;

namespace Identity.WebApi.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;

    public ProfileService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Profile GetProfile(string email)
    {
        var user = _userManager.Users.First(user => user.Email == email);
        var profile = new Profile
        {
            Firstname = user.FirstName,
            Lastname = user.LastName,
            Username = user.UserName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email
        };
        return profile;
    }
    
}