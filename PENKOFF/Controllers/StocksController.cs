using Microsoft.AspNetCore.Mvc;

namespace PENKOFF.Controllers;

public class StocksController : Controller
{
    // GET
    public IActionResult Stocks()
    {
        return View();
    }
}