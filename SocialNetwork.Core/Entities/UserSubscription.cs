namespace SocialNetwork.Core.Entities
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriptionUserId { get; set; }
    }
}
