using System.ComponentModel.DataAnnotations;
using Instagram.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.ViewModels.User;

public class UserRegisterVm
{
    [Required(ErrorMessage = "Поле обязательно")]
    [Remote(action: "EmailCheck", controller: "Validation", ErrorMessage ="Эта почта уже занята")]
    [Display(Name = "Адрес электронной почты")]
    public string Email { get; set; }
    
    
    [Required(ErrorMessage = "Поле обязательно")]
    [Remote( "CheckName", "Validation", ErrorMessage ="Этот логин уже занят")]
    [Display(Name = "Логин")]
    public string UserName { get; set; }
    
    
    [Required(ErrorMessage = "Поле обязательно")]
    public IFormFile  Avatar { get; set; }
    
    
    [Required(ErrorMessage = "Поле обязательно")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Поле обязательно")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public Gender? Gender { get; set; }
    
    public int PostCount { get; set; }
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
}