namespace SocialNetwork.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = string.Empty;

    public virtual Post Post { get; set; }
    public virtual User User { get; set; }
}
