using Instagram.Models;
using Instagram.Persistence;
using Instagram.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers;

public class PostController: Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public PostController(AppDbContext db, 
        UserManager<User> userManager, 
        SignInManager<User> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(PostAddVm model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();
        
        byte[] imageData = null;
        
        if (model.Image.Length > 0)
        {
            using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)model.Image.Length);
            }
        }

        Post? post = new Post(
            imageData,
            model.Description,
            user.Id
        );
        
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();

        return RedirectToAction("Profile", "User", new { userId = user.Id });
    }
    
    
}