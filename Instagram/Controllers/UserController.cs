using Instagram.Models;
using Instagram.Persistence;
using Instagram.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Instagram.Controllers;

public class UserController: Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IWebHostEnvironment _environment;

    public UserController(AppDbContext db, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IWebHostEnvironment environment)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _environment = environment;
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
                /*avatarFileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);

                string uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);

                string filePath = Path.Combine(uploadPath, avatarFileName); */

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

        var vm = new UserProfileVm
        {
            Avatar = Convert.ToBase64String(user.Avatar)
        };
        
        return View(vm);
    }
    
    
    /*[HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new UserLoginVm { ReturnUrl = returnUrl });
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginVm model)
    {
        if (ModelState.IsValid)
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
        }
        return View(model);
    }*/

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}