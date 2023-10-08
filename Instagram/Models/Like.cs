using Instagram.Models.Common;

namespace Instagram.Models;

public sealed class Like: Entity
{
    public string UserId { get; }
    public User? User { get; set; }
    
    public long PostId { get; }
    public Post? Post { get; set; }

    public Like(
        string userId,
        long postId)
    {
        UserId = userId;
        PostId = postId;
    }
    
    private Like(){}
}