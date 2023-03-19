namespace SocialNetwork.Core.Entities.Chat;

public class Chat
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<Participant> Participants { get; set; }
 }
