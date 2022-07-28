using Logic.PENKOFF;
using Microsoft.AspNetCore.Mvc;
using PENKOFF.Models;

namespace PENKOFF.Controllers;

public class AccountController : Controller
{
    private readonly IUserManager _manager;
    public AccountController(IUserManager manager)
    {
        _manager = manager;
    }
    // GET
    public async Task<IActionResult> Account()
    {
        if (HttpContext.Session.GetInt32("Id") == null)
        {
            return View("~/Views/Account/Login.cshtml");
        }
        
        return View();
    }

    public async Task<IActionResult> Login(LoginViewModel model)
    {
         
        return View();
    }
}