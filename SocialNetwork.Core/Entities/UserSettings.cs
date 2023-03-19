namespace SocialNetwork.Core.Entities;

public class UserSettings
{
    public int UserId { get; set; }
    public bool IsShowBirthDate { get; set; } = true;
    public bool IsShowInterests { get; set; } = true;
    public bool IsProfileHidden { get; set; }
}
