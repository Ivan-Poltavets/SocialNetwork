using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Core.Services;

public class FileService
{
    private readonly IFileRepository _fileRepository;

    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public byte[] IFormFileToByteArray(IFormFile formFile)
    {
        byte[] fileByteArray;

        using (var memoryStream = new MemoryStream())
        {
            formFile.CopyTo(memoryStream);
            fileByteArray = memoryStream.ToArray();
        }

        return fileByteArray;
    }

    public async Task<string> UploadFileAsync(string folder, IFormFile formFile)
    {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
        await _fileRepository.UploadAsync(folder, fileName, IFormFileToByteArray(formFile));
        return $"{folder}/{fileName}";
    }

    
}
