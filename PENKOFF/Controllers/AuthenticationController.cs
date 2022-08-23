using System.Security.Claims;
using Logic.PENKOFF;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PENKOFF.Models;

namespace PENKOFF.Controllers;

public class AuthenticationController : Controller
{
    private readonly IUserManager _manager;
    
    public AuthenticationController(IUserManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        /*UserService us = new UserService(_manager);
        var response = await us.Login(model);
        if (response.StatusCode == Enums.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));
            return RedirectToAction("Account");
        }
        ModelState.AddModelError("", response.Description);
        return View(model);*/
        var user = _manager.FindUser(model.user.Login, Security.HashPassword(model.user.Password));
        if (user == null)
        {
            return View(new LoginViewModel()
            {
                result = "Incorrect Login or Password"
            });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.user.Login)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Login");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        /*return RedirectToAction("Account", "Account");*/
        return RedirectToAction("Account", "Account");
    }

    [HttpGet]
    public IActionResult Register() => View("Register", new RegisterVewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVewModel model)
    {
        if (model.user.Password != model.ConfirmPassword)
        {
            return View("Register", new RegisterVewModel
            {
                result = "Passwords does not match"
            });
        }

        var us = new UserService(_manager);
        var response = await us.Register(model);
        if (response.StatusCode != Enums.StatusCode.OK)
            return View("Register", new RegisterVewModel()
            {
                result = "Something went wrong"
            });
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(response.Data));

        return View("Login", new LoginViewModel());

    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}