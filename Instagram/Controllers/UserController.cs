using Instagram.Models;
using Instagram.Persistence;
using Instagram.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Instagram.Controllers;

public class UserController: Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserController(AppDbContext db, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterVm model)
    {
        /*if (ModelState.IsValid)
        {*/
            byte[] imageData = null;
            if (model.Avatar.Length > 0)
            {
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }
            }

            User? user = new User(
                model.UserName,
                model.Email,
                imageData,
                model.Password,
                model.Name,
                model.Description,
                model.Gender,
                model.PhoneNumber);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        //}

        return RedirectToAction("Index", "Home");
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Profile(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound();

        var posts = _db.Posts.Where(p => p.UserId == user.Id)
            .Select(p => p.Image != null ? Convert.ToBase64String(p.Image) : null)
            .ToList();
        
        var vm = new UserProfileVm
        {
            UserName = user.UserName,
            Info = user.Description,
            Avatar = Convert.ToBase64String(user.Avatar),
            PostCount = user.Posts.Count,
            FollowerCount = user.Followers.Count,
            FollowingCount = user.Subscriptions.Count,
            Posts = posts
        };
        
        return View(vm);
    }
    
    
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new UserLoginVm { ReturnUrl = returnUrl });
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginVm model)
    {
        User user = await _userManager.FindByEmailAsync(model.Email);
        SignInResult result = await _signInManager.PasswordSignInAsync(
            user,
            model.Password,
            model.RememberMe,
            false
        );

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
    
    return View(model);
        
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}