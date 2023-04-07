using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Services;
using SocialNetwork.WebUI.Models;
using SocialNetwork.Core.Dtos;

namespace SocialNetwork.WebUI.Controllers
{
    public class PostsController : AuthorizeController
    {
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly FileService _fileService;

        public PostsController(PostService postService, UserService userService, FileService fileService)
        {
            _postService = postService;
            _userService = userService;
            _fileService = fileService;
        }

        [HttpGet("[controller]/[action]/{userName}")]
        public IActionResult GetUserPosts(string userName, int pageIndex = 0, int itemCount = 10)
        {
            var posts = _postService.GetUserPosts(_userService.GetUserIdByUserName(userName), pageIndex, itemCount);
            return Ok(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserPost(AddUserPostDto addUserPostDto)
        {
            string? fileName = null;
            if(addUserPostDto.MediaFile != null)
            {
                fileName = await _fileService.UploadFileAsync($"/{UserName}", addUserPostDto.MediaFile);
            }

            await _postService.AddUserPost(UserId, new UserPostDto
            {
                FileName = fileName,
                Description = addUserPostDto.Description
            });

            return Redirect($"/Users/Index/{UserName}");
        }

        public IActionResult AddPostPartial()
        {
            return PartialView("_AddPost");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteUserPost(string username, int postId)
        {
            if(UserName == username)
            {
                await _postService.DeleteUserPost(postId);
                return NoContent();
            }
            return BadRequest();
        }
    }
}
