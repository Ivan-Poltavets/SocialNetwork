using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Dtos;

public class UserInformationDto
{
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Bio { get; set; }
    public string? Interests { get; set; }
}
