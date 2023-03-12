#region

using System;
using System.IO;
using System.Threading.Tasks;

#endregion

namespace PhotoContest.Implementation.Service.Files;

/// <summary>
/// </summary>
public class SystemFileProvider : IFileProvider
{
    /// <summary>
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Stream ReadFileAsync(string filename)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        path = Path.Combine(path, filename);
        return File.OpenRead(path);
    }

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="filename"></param>
    public async Task UploadFileAsync(Stream stream, string filename)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/");

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        await using var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create);
        await stream.CopyToAsync(fileStream);
    }
}