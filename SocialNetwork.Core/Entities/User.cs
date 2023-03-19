namespace SocialNetwork.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Interests { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
