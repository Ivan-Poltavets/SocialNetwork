using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Core.Services;

public class UserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;
    private readonly ICustomMapper _mapper;
    private readonly IFileRepository _fileRepository;

    public UserService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileRepository fileRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.GetRepository<User>();
        _mapper = mapper;
        _fileRepository = fileRepository;
    }

    public int GetUserIdByUserName(string username)
    {
        var user = _userRepository.Find(x => x.UserName == username).SingleOrDefault();
        if(user is null)
        {
            throw new Exception();
        }
        return user.Id;
    }

    public UserInformationDto GetUserInformation(string username)
    {
        var user = _userRepository.Find(x => x.UserName == username).SingleOrDefault();
        if(user is null)
        {
            throw new Exception();
        }
        var userInformationDto = _mapper.Map<UserInformationDto>(user);
        return userInformationDto;
    }

    public async Task UpdateUserInformationAsync(int userId, UserInformationDto userInformationDto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if(user != null)
        {
            user = _mapper.Map(userInformationDto, user);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<byte[]> GetUserImageAsync(string username)
    {
        var user = _userRepository.Find(x => x.UserName == username).SingleOrDefault();
        return await _fileRepository.DownloadAsync(string.Empty, user.ImageName);
    }

    public async Task UpdateUserImageAsync(int userId, string fileExtension, byte[] fileByteArray)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if(user != null)
        {
            if (!string.IsNullOrEmpty(user.ImageName))
            {
                await _fileRepository.DeleteAsync(string.Empty, user.ImageName);
            }
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            await _fileRepository.UploadAsync(string.Empty, fileName, fileByteArray);
            user.ImageName = fileName;
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public List<User> SearchUsers(string query)
    {
        return _userRepository.Get(itemCount: 20).Where(x => x.UserName.StartsWith(query)).ToList();
    }
}
