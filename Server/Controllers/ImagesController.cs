using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Server.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Server.Controllers
{
    /// <summary>
    /// Image Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Initializes new Image Controller
        /// </summary>
        /// <param name="_webHostEnvironment"></param>
        public ImagesController(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        /// <summary>
        /// Api to save an image
        /// </summary>
        /// <param name="imageItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveImage([FromForm] ImageItem imageItem)
        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "Images/");

            var referenceId = Guid.NewGuid().ToString();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fileStream = new FileStream(Path.Combine(path, referenceId), FileMode.Create))
            {
                await imageItem.Image.CopyToAsync(fileStream);
            }
            return Ok(imageItem);
        }

        /// <summary>
        /// Gets image specified by the reference id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{referenceId}")]
        public IActionResult Get(string referenceId)
        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "images/");

            byte[] b = System.IO.File.ReadAllBytes(Path.Combine(path, referenceId));

            return File(b, "image/jpg");
        }
    }
}
