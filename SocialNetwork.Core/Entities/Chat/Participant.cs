namespace SocialNetwork.Core.Entities.Chat;

public class Participant
{
    public int UserId { get; set; }
    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }
    public virtual User User { get; set; }
}
