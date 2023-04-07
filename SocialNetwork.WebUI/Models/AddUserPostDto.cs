namespace SocialNetwork.WebUI.Models
{
    public class AddUserPostDto
    {
        public IFormFile? MediaFile { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
