using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Core.Services;

public class PostService
{
    private readonly ICustomMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<UserPost> _userPostRepository;
    private readonly IFileRepository _fileRepository;

    public PostService(ICustomMapper mapper, IUnitOfWork unitOfWork, IFileRepository fileRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userPostRepository = _unitOfWork.GetRepository<UserPost>();
        _fileRepository = fileRepository;
    }

    public async Task AddUserPost(int userId, UserPostDto userPostDto)
    {
        var userPost = new UserPost
        {
            UserId = userId
        };
        userPost = _mapper.Map(userPostDto, userPost);
        await _userPostRepository.AddAsync(userPost);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<UserPostDto> GetUserPost(int postId)
    {
        var post = await _userPostRepository.GetByIdAsync(postId);
        if(post != null)
        {
            return _mapper.Map<UserPostDto>(post);
        }
        throw new Exception();
    }

    public List<UserPostDto> GetUserPosts(int userId, int pageIndex = 0, int itemCount = 10)
    {
        var posts = _userPostRepository.Find(x => x.UserId == userId);
        if (posts.Any())
        {
            posts
            .Skip(pageIndex * itemCount)
            .Take(itemCount)
            .ToList();
            return _mapper.Map<List<UserPostDto>>(posts);
        }

        return new List<UserPostDto>();
    }

    public async Task<byte[]> GetPostImage(string folder, string file)
    {
        return await _fileRepository.DownloadAsync(folder, file);
    }

    public async Task DeleteUserPost(int postId)
    {
        var post = await _userPostRepository.GetByIdAsync(postId);
        if(post == null)
            return;

        if(post.FileName != null)
            await _fileRepository.DeleteAsync(string.Empty, post.FileName);

        _userPostRepository.Delete(post);
        await _unitOfWork.SaveChangesAsync();
    }
}
