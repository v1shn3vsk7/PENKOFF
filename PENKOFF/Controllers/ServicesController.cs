using Microsoft.AspNetCore.Mvc;

namespace PENKOFF.Controllers;

public class ServicesController : Controller
{
    
    public IActionResult Services()
    {
        return View();
    }
}