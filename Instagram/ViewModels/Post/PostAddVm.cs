using System.ComponentModel.DataAnnotations;

namespace Instagram.ViewModels.Post;

public class PostAddVm
{
    [Required(ErrorMessage = "Поле обязательно")]
    public IFormFile Image { get; set; }
    
    [Required(ErrorMessage = "Поле обязательно")]
    public string Description { get; set; }
}