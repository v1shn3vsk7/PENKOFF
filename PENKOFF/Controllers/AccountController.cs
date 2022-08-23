using System.Data.Entity;
using Logic.PENKOFF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PENKOFF.Models;


namespace PENKOFF.Controllers;

[Authorize]
public class AccountController : Controller
{
    // GET
    /*public async Task<IActionResult> Account() => User.Identity.IsAuthenticated ?
        View() : View("~/Views/Account/Login.cshtml", new LoginViewModel());*/
    public IActionResult Account()
    {
        /*var authState = await authenticationStateTask;
        var user = authState.User;
        
        return user.Identity.IsAuthenticated ? View() : View("~/Views/Account/Login.cshtml", new LoginViewModel());*/
        return View();
    }
}