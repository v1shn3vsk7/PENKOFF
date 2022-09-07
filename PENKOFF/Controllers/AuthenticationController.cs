using System.Security.Claims;
using Logic.PENKOFF;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PENKOFF.Models;
using Storage.Entities;
using Storage.Enums;

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
        var user = await _manager.FindUser(model.user.Login, Security.HashPassword(model.user.Password));
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

        return user.Role switch
        {
            Role.Admin => RedirectToAction("AdminAccount", "Account"),
            Role.TechSupport => RedirectToAction("TechSupportAccount", "Account"),
            Role.User => RedirectToAction("Account", "Account"),
            _ => View(new LoginViewModel() {result = "Incorrect Login or Password"})
        };
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
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}