namespace Instagram.ViewModels.Home;

public class HomeVm
{
    public bool Liked = false;
    public string CurrentUserId { get; set; }
    public string UserComment { get; set; }
    public List<Models.Post> Posts { get; set; } = new();
}