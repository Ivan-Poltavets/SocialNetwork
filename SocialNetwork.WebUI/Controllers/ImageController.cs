using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Services;

namespace SocialNetwork.WebUI.Controllers
{
    public class ImageController : Controller
    {
        private readonly UserService _userService;
        private readonly PostService _postService;

        public ImageController(UserService userService, PostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        [HttpGet("[controller]/{username}")]
        public async Task<IActionResult> GetUserImage(string userName)
        {
            var user = _userService.GetUserInformation(userName);
            var content = await _userService.GetUserImageAsync(userName);
            return File(content, GetImageMimeTypeFromFileExtension(Path.GetExtension(user.ImageName)));
        }

        [HttpGet("[controller]/{userName}/posts/{fileName}")]
        public async Task<IActionResult> GetPostImage(string userName, string fileName)
        {
            var content = await _postService.GetPostImage($"/{userName}", fileName);
            return File(content, GetImageMimeTypeFromFileExtension(Path.GetExtension(fileName)));
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
