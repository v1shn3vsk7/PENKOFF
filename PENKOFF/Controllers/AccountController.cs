using Microsoft.AspNetCore.Mvc;

namespace PENKOFF.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Account()
    {
        return View();
    }
}