using Instagram.Models;

namespace Instagram.ViewModels.User;

public class UserProfileVm
{
    public string UserName { get; set; }
    public string Info { get; set; }
    public int PostCount { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public string Avatar { get; set; }
    public List<string> Posts { get; set; } = new();
}