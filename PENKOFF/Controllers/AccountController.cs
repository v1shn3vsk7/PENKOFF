﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PENKOFF.Controllers;

[Authorize]
public class AccountController : Controller
{
    public IActionResult Account()
    {
        return View();
    }
}