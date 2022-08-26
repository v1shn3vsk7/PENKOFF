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
    public IActionResult Account()
    {
        return View();
    }
}