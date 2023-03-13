#region

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
///     Image Controller
/// </summary>
public interface IFileController
{
    /// <summary>
    ///     Gets image specified by the reference id
    /// </summary>
    /// <returns></returns>
    IActionResult Get(int id);

    /// <summary>
    ///     Api to save an image
    /// </summary>
    /// <param name="imageFileRequest"></param>
    /// <returns></returns>
    Task<IActionResult> SaveImage([FromForm] ImageFileRequest imageFileRequest);
}