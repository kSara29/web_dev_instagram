using Instagram.Models.Common;

namespace Instagram.Models;

public class Subscription: Entity
{
    public string TargetUserId { get; }
    public User? TargetUser { get; set; }
    
    public string SubscriberId { get; }
    public User? Subscriber { get; set; }

    public Subscription(
        string targetUserId, 
        string subscriberId)
    {
        TargetUserId = targetUserId;
        SubscriberId = subscriberId;
    }
    
    private Subscription(){}
    
}