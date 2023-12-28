using Microsoft.AspNetCore.Identity;

namespace Identity.WebApi.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }
}