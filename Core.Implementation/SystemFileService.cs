using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Implementation
{
    public class SystemFileService : IFileService
    {
        public Stream ReadFileAsync(string filename)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images/");
            return File.OpenRead(path);
        }

        public async Task UploadFileAsync(Stream stream, string filename)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/");
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create);
            await stream.CopyToAsync(fileStream);
        }
    }
}
