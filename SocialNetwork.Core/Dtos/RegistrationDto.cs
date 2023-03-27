namespace SocialNetwork.Core.Dtos;

public class RegistrationDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Bio { get; set; }
    public string? Interests { get; set; }
    public string Password { get; set; } = string.Empty;
}
