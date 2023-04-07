using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Services;
using System.Security.Claims;

namespace SocialNetwork.WebUI.Controllers;

public class AccessController : Controller
{
    private readonly AccessService _accessService;

    public AccessController(AccessService accessService)
    {
        _accessService = accessService;
    }

    public IActionResult Login()
    {
        var claimUser = HttpContext.User;
        if (claimUser.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = _accessService.VerifyLogin(loginDto);
        if (user != null)
        {
            await SignInAsync(user.Id.ToString(), user.UserName);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationDto registrationDto)
    {
        var user = await _accessService.RegistrationAsync(registrationDto);
        if(user != null)
        {
            await SignInAsync(user.Id.ToString(), user.UserName);

            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Access");
    }

    private async Task SignInAsync(string userId, string userName)
    {
        var claimsIdentity = _accessService.CreateClaimsIdentity(userId, userName, CookieAuthenticationDefaults.AuthenticationScheme);
        var authenticationProperties = new AuthenticationProperties()
        {
            IsPersistent = true,
            AllowRefresh = true
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authenticationProperties);
    }
}
