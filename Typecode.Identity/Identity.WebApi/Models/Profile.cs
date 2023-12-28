namespace Identity.WebApi.Models;

public class Profile
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
}