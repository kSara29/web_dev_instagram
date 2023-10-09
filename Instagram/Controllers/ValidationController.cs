using Instagram.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers;

public class ValidationController
{
    private  AppDbContext _context;
    
    public ValidationController(AppDbContext context)
    {
        _context = context;
    }
    
    [AcceptVerbs("GET", "POST")]
    public bool CheckName(string userName)
    {
        var check = _context.Users.Any(u => u.UserName.ToLower() == userName.ToLower());
        return !check;
    }
    
    [AcceptVerbs("GET", "POST")]
    public bool CheckEmail(string userEmail)
    {
        var check = _context.Users.Any(u => u.Email.ToLower() == userEmail.ToLower());
        return !check;
    }

    /*[AcceptVerbs("GET", "POST")]
    public bool CheckNameEdit(string name, string Id)
    {
        if (long.TryParse(Id, out long idValue))
        {
            var check = _context.Brands.Any(b => b.Name.ToLower() == name.ToLower() && b.Id != idValue);
            return !check;
        }
        
        return false;
    }*/
}