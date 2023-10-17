using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Instagram.Models;
using Instagram.Persistence;
using Instagram.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;

    public HomeController(AppDbContext db, 
        UserManager<User> userManager,
        ILogger<HomeController> logger)
    {
        _logger = logger;
        _db = db;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        
        var followingList = _db.Subscriptions.Where(s => s.SubscriberId == user.Id)
            .Select(s => s.TargetUserId)
            .ToList();

        var posts = _db.Posts.Where(p => followingList.Contains(p.UserId))
            .OrderByDescending(p => p.CreatedAt)
            .Include(p => p.User)
            .Include(p => p.Comments.OrderByDescending(c => c.CreatedAt))
            .Include(p => p.Likes)
            .ToList();
        
        foreach (var post in posts)
        {
            foreach (var comment in post.Comments)
            {
                _db.Entry(comment)
                    .Reference(c => c.User)
                    .Load();
            }
        }
        
        

        var vm = new HomeVm
        {
            CurrentUserId = user.Id,
            Posts = posts
        };

        return View(vm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(HomeVm vm, long postId)
    {
        var user = await _userManager.GetUserAsync(User);
        var like = new Like(user.Id, postId);
        
        if (vm.UserComment is not null)
        {
            var comment = new PostComment(postId, user.Id, vm.UserComment);
            await _db.PostComments.AddAsync(comment);
            await _db.SaveChangesAsync();
        }
        
        var likeCheck = _db.Likes
            .FirstOrDefault(s => s.UserId == user.Id && s.PostId == postId);

        bool check = false;

        if (likeCheck != null)
        {
            _db.Likes.Remove(likeCheck);
            await _db.SaveChangesAsync();
            check = false;
        }
        else
        {
            await _db.Likes.AddAsync(like);
            await _db.SaveChangesAsync();
            check = true;
        }
        
        var followingList = _db.Subscriptions.Where(s => s.SubscriberId == user.Id)
            .Select(s => s.TargetUserId)
            .ToList();

        var posts = _db.Posts.Where(p => followingList.Contains(p.UserId))
            .OrderByDescending(p => p.CreatedAt)
            .Include(p => p.User)
            .Include(p => p.Comments.OrderByDescending(c => c.CreatedAt))
            .Include(p => p.Likes)
            .ToList();
        
        foreach (var post in posts)
        {
            foreach (var comment in post.Comments)
            {
                _db.Entry(comment)
                    .Reference(c => c.User)
                    .Load();
            }
        }
        
        vm = new HomeVm
        {
            CurrentUserId = user.Id,
            Posts = posts
        };

        return View(vm);
    }
  
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}