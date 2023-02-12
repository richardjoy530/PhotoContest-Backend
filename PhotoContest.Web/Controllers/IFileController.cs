#region

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Controllers;

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
    /// <param name="imageItem"></param>
    /// <returns></returns>
    Task<IActionResult> SaveImage([FromForm] ImageItem imageItem);
}