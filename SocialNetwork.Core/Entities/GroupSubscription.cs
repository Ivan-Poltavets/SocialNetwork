namespace SocialNetwork.Core.Entities;

public class GroupSubscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubscriptionGroupId { get; set; }
}
