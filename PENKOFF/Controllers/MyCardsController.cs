using Microsoft.AspNetCore.Mvc;

namespace PENKOFF.Controllers;

public class MyCardsController : Controller
{
    // GET
    public IActionResult MyCards()
    {
        return View();
    }
}