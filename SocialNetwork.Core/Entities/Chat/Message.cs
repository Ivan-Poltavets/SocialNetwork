namespace SocialNetwork.Core.Entities.Chat;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int? ReceiverId { get; set; }
    public int? ChatId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime SendDate { get; set; }

    public virtual Chat Chat { get; set; }
}
