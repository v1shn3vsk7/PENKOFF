using Microsoft.AspNetCore.Mvc;

namespace PENKOFF.Controllers;

public class AccountController : Controller
{
    // GET
    public async Task<IActionResult> Account()
    {
        if (HttpContext.Session.GetInt32("Id") == null)
        {
            return View("~/Views/Account/Login.cshtml");
        }
        
        return View();
    }
}