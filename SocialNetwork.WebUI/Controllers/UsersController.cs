using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Services;
using System.Security.Claims;

namespace SocialNetwork.WebUI.Controllers
{
    [Authorize]
    public class UsersController : AuthorizeController
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[controller]/[action]/{username}")]
        public async Task<IActionResult> Index(string userName)
        {
            var user = _userService.GetUserInformation(userName);
            return View(user);
        }

        public async Task<IActionResult> Edit()
        {
            var user = _userService.GetUserInformation(UserName);
            return View(user);
        }

        public IActionResult Search(List<User>? users) =>
            View(users);

        [HttpPost]
        public IActionResult Search(string query) =>
            View(_userService.SearchUsers(query));

        [HttpPost]
        public async Task<IActionResult> UpdateUserInformation(UserInformationDto userInformationDto)
        {
            await _userService.UpdateUserInformationAsync(UserId, userInformationDto);
            return Redirect($"/Users/Index/{UserName}");
        }

        [HttpPost]
        public async Task<IActionResult> UploadUserImage(IFormFile formFile)
        {
            byte[] fileByteArray;
            if(formFile != null)
            {
                using(var item = new MemoryStream())
                {
                    formFile.CopyTo(item);
                    fileByteArray = item.ToArray();
                }
                await _userService.UpdateUserImageAsync(UserId, Path.GetExtension(formFile.FileName), fileByteArray);
            }
            return Redirect($"/Users/Index/{UserName}");
        }

        
    }
}
