namespace SocialNetwork.Core.Interfaces
{
    public interface IFileRepository
    {
        public Task UploadAsync(string folder, string file, byte[] content);
        public Task DeleteAsync(string folder, string file);
        public Task<byte[]> DownloadAsync(string folder, string file);
        public Task ListRootFolder();
    }
}
