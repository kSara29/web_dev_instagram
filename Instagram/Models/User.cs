using Instagram.Enums;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Models;

public sealed class User: IdentityUser
{
    public string Avatar { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Gender? Gender { get; set; }
    public List<Post> Posts { get; set; } = new();
    public List<Subscription> Subscriptions { get; set; } = new();
    public List<Subscription> Followers { get; set; } = new();

    public User(
        string login,
        string email,
        string avatar,
        string? name = null,
        string? description = null,
        Gender? gender = null,
        string? phoneNumber = null
    )
    {
        UserName = login;
        Email = email;
        Avatar = avatar;
        Name = name;
        Description = description;
        Gender = gender;
        PhoneNumber = phoneNumber;
    }
    
    private User(){}
}