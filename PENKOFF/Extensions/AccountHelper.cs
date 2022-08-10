using System.Security.Claims;
using Logic.PENKOFF;
using PENKOFF.Enums;
using PENKOFF.Models;
using PENKOFF.Response;
using Storage.Entities;

namespace PENKOFF;

public class UserService
{
    private readonly IUserManager _manager;
    public UserService(IUserManager manager)
    {
        _manager = manager;
    }
    public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterVewModel model)
    {
        try
        {
            var user = await _manager.FindUser(model.user.Login);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Login already exists",
                };
            }

            user = new User()
            {
                FirstName = model.user.FirstName,
                LastName = model.user.LastName,
                Login = model.user.Login,
                Password = Security.HashPassword(model.user.Password)
            };

            await _manager.Create(user);
            var result = Authenticate(user);
            
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch(Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
    {
        try
        {
            var user = await _manager.FindUser(model.user.Login,
                Security.HashPassword(model.user.Password));
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "User is not found"
                };
            }
            var result = Authenticate(user);
            
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    private static ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}