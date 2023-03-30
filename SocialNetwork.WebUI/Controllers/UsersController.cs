using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Services;
using System.Security.Claims;

namespace SocialNetwork.WebUI.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        private string UserName => User.FindFirst(ClaimTypes.Name).Value;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[controller]/[action]/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var user = await _userService.GetUserInformationAsync(username);
            return View(user);
        }

        public async Task<IActionResult> Edit()
        {
            var user = await _userService.GetUserInformationAsync(UserName);
            return View(user);
        }

        public IActionResult Search()
        {
            return PartialView();
        }

        [HttpGet("[controller]/[action]/{query}")]
        public IActionResult Search(string query)
        {
            return Ok(_userService.SearchUsers(query));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInformation(UserInformationDto userInformationDto)
        {
            await _userService.UpdateUserInformationAsync(UserId, userInformationDto);
            return RedirectToAction("Index", UserName);
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
            return RedirectToAction("Index", UserName);
        }

        [HttpGet("[controller]/[action]/{username}")]
        public async Task<IActionResult> GetUserImage(string username)
        {
            var user = await _userService.GetUserInformationAsync(username);
            var content = await _userService.GetUserImageAsync(username);
            return File(content, GetImageMimeTypeFromFileExtension(Path.GetExtension(user.ImageName)));
        }

        private string GetImageMimeTypeFromFileExtension(string extension)
        {
            string mimetype = extension switch
            {
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".bmp" => "image/bmp",
                ".tiff" => "image/tiff",
                ".wmf" => "image/wmf",
                ".jp2" => "image/jp2",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream",
            };
            return mimetype;
        }
    }
}
