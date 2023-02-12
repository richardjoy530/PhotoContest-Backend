using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PhotoContest;

/// <summary>
/// 
/// </summary>
public interface IFileService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    Task SaveFile(IFormFile file);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Stream GetFile(int id);

}