using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Configuration;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Infrastructure.Repository
{
    internal class FileRepository : IFileRepository
    {
        private readonly string _accessKey;
        private DropboxClient _dropboxClient;

        public FileRepository(IConfiguration configuration)
        {
            _accessKey = configuration.GetConnectionString("DropboxAccessKey");
        }

        public async Task UploadAsync(string folder, string file, byte[] content)
        {
            _dropboxClient = new DropboxClient(_accessKey);
            using(var mem = new MemoryStream(content))
            {
                var updated = await _dropboxClient.Files.UploadAsync(
                    folder + "/" + file, 
                    WriteMode.Overwrite.Instance,
                    body: mem
                    );
                Console.WriteLine(file, updated.Rev);
            }
            _dropboxClient.Dispose();
        }

        public async Task DeleteAsync(string folder, string file)
        {
            _dropboxClient = new DropboxClient(_accessKey);
            if (!file.StartsWith("/"))
                file = $"/{file}";
            await _dropboxClient.Files.DeleteV2Async(folder + file);
            _dropboxClient.Dispose();
        }

        public async Task<byte[]> DownloadAsync(string folder, string file)
        {
            _dropboxClient = new DropboxClient(_accessKey);
            byte[] content;
            using (var response = await _dropboxClient.Files.DownloadAsync(folder + "/" + file))
            {
                content = await response.GetContentAsByteArrayAsync();
            }
            _dropboxClient.Dispose();
            return content;
        }

        public async Task ListRootFolder()
        {
            _dropboxClient = new DropboxClient(_accessKey);
            var list = await _dropboxClient.Files.ListFolderAsync(string.Empty);

            foreach (var item in list.Entries.Where(i => i.IsFolder))
            {
                Console.WriteLine("D  {0}/", item.Name);
            }

            foreach (var item in list.Entries.Where(i => i.IsFile))
            {
                Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
            }
        }
    }
}
