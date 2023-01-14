#region

using Microsoft.AspNetCore.Http;

#endregion

namespace PhotoContest;

/// <summary>
///     An interface for data access on files
/// </summary>
public interface IFileProvider
{
    /// <summary>
    ///     Saves the given file into the file system
    /// </summary>
    /// <param name="file"></param>
    /// <param name="referenceId"></param>
    /// <remarks>File will be saved in the File system of the WebApi server, the file location will be saved in database</remarks>
    /// <returns>referenceId of the file</returns>
    public string UploadImage(IFormFile file, string referenceId);

    /// <summary>
    ///     Retrives the file contents as a byte[]
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns></returns>
    public byte[] GetFileDataInBytes(string referenceId);
}