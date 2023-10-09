using System.ComponentModel.DataAnnotations;

namespace Instagram.ViewModels.User;

public class UserLoginVm
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
    
    
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; }
    

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    
    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
    
    public string ReturnUrl { get; set; }
}