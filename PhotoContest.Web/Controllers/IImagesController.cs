using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;
using System.Threading.Tasks;

namespace PhotoContest.Web.Controllers
{
    /// <summary>
    /// Image Controller
    /// </summary>
    public interface IImagesController
    {
        /// <summary>
        /// Gets image specified by the reference id
        /// </summary>
        /// <returns></returns>
        IActionResult Get(string referenceId);

        /// <summary>
        /// Api to save an image
        /// </summary>
        /// <param name="imageItem"></param>
        /// <returns></returns>
        Task<IActionResult> SaveImage([FromForm] ImageItem imageItem);
    }
}