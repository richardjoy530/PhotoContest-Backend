using System.IO;
using System.Threading.Tasks;

namespace PhotoContest
{
    public interface IFileService
    {
        Stream ReadFileAsync(string filename);

        Task UploadFileAsync(Stream stream, string filename);
    }
}