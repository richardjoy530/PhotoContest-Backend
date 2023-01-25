#region

using System.IO;
using System.Threading.Tasks;

#endregion

namespace PhotoContest;

/// <summary>
/// </summary>
public interface IFileService
{
    /// <summary>
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    Stream ReadFileAsync(string filename);

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    Task UploadFileAsync(Stream stream, string filename);
}