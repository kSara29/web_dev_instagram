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
            .Include(p => p.User)
            .ToList();

        var vm = new HomeVm
        {
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