namespace SocialNetwork.Core.Dtos;

public class UserPostDto
{
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string Description { get; set; } = string.Empty;
}
