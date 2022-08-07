using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using Logic.PENKOFF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PENKOFF.Models;
using Storage.Entities;
using Storage.Enums;

namespace PENKOFF.Controllers;

public class AccountController : Controller
{
    private readonly IUserManager _manager;

    public AccountController(IUserManager manager)
    {
        _manager = manager;
    }

    // GET
    public async Task<IActionResult> Account()
    {
        if (HttpContext.Session.GetInt32("Id") == null)
        {
            return View("~/Views/Account/Login.cshtml", new LoginViewModel
            {
                result = ""
            });
        }

        return View();
    }

    public AuthenticateResponse Authenticate(LoginViewModel model)
    {
        var user = _manager.GetAll().FirstOrDefault(User => User.Login == model.user.Login && User.Password == Security.HashPassword(model.user.Password));
        
        if (user == null)
        {
            return null;
        }

        var token = UserHelper.GenerateJwtToken(user);
        
        return new AuthenticateResponse(user, token);
    }

    public async Task<IActionResult> Login(LoginViewModel model)
    {
        /*var user = await _manager.GetAll().FirstOrDefaultAsync(User => User.Login == model.user.Login);

        if (user is null)
        {
            return View("Login", new LoginViewModel
            {
                result = "Incorrect login or password"
            });
        }

        if (user.Password != Security.HashPassword(model.user.Password))
        {
            return View("Login", new LoginViewModel
            {
                result = "Incorrect login or password"
            });
        }

        //HttpContext.Session.SetInt32("Id", user.Id);
        return View("Account");*/

        var response = Authenticate(model);
        
        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });
        
        return Ok(response);
    }

    public IActionResult SignUp()
    {
        return View("SignUp", new SignUpVewModel
        {
            result = ""
        });
    }

    public async Task<IActionResult> Registration(SignUpVewModel model)
    {
        if (model.user.Password != model.ConfirmPassword)
        {
            return View("SignUp", new SignUpVewModel
            {
                result = "Passwords does not match"
            });
        }

        var user = await _manager.FindUser(model.user.Login);

        if (user is not null)
        {
            return View("SignUp", new SignUpVewModel
            {
                result = "Login already exists"
            });
        }

        user = new User()
        {
            FirstName = model.user.FirstName,
            LastName = model.user.LastName,
            Login = model.user.Login,
            Password = Security.HashPassword(model.user.Password),
            Role = Role.User
        };

        await _manager.Create(user);

        HttpContext.Session.SetInt32("Id", _manager.GetUserId(model.user.Login));
        HttpContext.Session.SetInt32("Id", user.Id); ///////////////////////////////////////

        return View("MailVerification", new MailVerificationViewModel());
    }

    public IActionResult MailVerification(MailVerificationViewModel model)
    {
        Random rn = new();
        var verificationCode = rn.Next(100000, 999999);
        HttpContext.Session.SetInt32("verificationCode", verificationCode);

        Verification.SendEmail(model.mail, verificationCode);
        HttpContext.Session.SetString("Email", model.mail);

        return View("MailVerification", new MailVerificationViewModel
        {
            result = "",
            isCodeSent = true,
            mail = model.mail
        });
    }

    public async Task<IActionResult> CheckCode(MailVerificationViewModel model)
    {
        if (model.inputForVerificationCode != HttpContext.Session.GetInt32("verificationCode"))
        {
            return View("MailVerification", new MailVerificationViewModel
            {
                result = "Wrong code. Try again"
            });
        }

        await _manager.AddEmailToUser((int) HttpContext.Session.GetInt32("Id"),
            (string) HttpContext.Session.GetString("Email"));

        HttpContext.Session.Remove("verificationCode");
        HttpContext.Session.Remove("Email");

        return RedirectToAction("Account");
    }
}