using Identity.WebApi.Models;

namespace Identity.WebApi.Interfaces;

public interface IProfileService
{
    Profile GetProfile(string email);
}