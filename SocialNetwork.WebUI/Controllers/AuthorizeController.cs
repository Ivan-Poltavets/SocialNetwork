using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SocialNetwork.WebUI.Controllers
{
    [Authorize]
    public class AuthorizeController : Controller
    {
        protected int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        protected string UserName => User.FindFirst(ClaimTypes.Name).Value;
    }
}
