namespace SocialNetwork.Core.Entities;

public class Post
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int? GroupId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int LikesCount { get; set; } = 0;

    public virtual User User { get; set; }
    public virtual Group Group { get; set; }
}
