namespace Typecode.Domain;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    
    public string HashedPassword { get; set; }
}