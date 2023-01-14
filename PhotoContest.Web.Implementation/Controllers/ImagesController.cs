#region

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Models;
using PhotoContest.Web.Contracts;
using PhotoContest.Web.Controllers;

#endregion

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
///     Image Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase, IImagesController
{
    private readonly IProvider<FileMap> fileMapProvider;
    private readonly IFileService fileService;

    /// <summary>
    ///     Initializes new Image Controller
    /// </summary>
    /// <param name="_fileService"></param>
    /// <param name="_fileMapProvider"></param>
    public ImagesController(
        IFileService _fileService,
        IProvider<FileMap> _fileMapProvider)
    {
        fileService = _fileService ?? throw new ArgumentNullException(nameof(_fileService));
        fileMapProvider = _fileMapProvider ?? throw new ArgumentNullException(nameof(_fileMapProvider));
    }

    /// <summary>
    ///     Api to save an image
    /// </summary>
    /// <param name="imageItem"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SaveImage([FromForm] ImageItem imageItem)
    {
        if (string.IsNullOrWhiteSpace(imageItem.ReferenceId))
            imageItem.ReferenceId = Guid.NewGuid().ToString();
        else if (!Guid.TryParse(imageItem.ReferenceId, out _))
            throw new ValidationException($"Invalid {nameof(imageItem.ReferenceId)}");

        var fileMap = new FileMap(imageItem.ReferenceId) {FilePath = imageItem.Image.FileName};

        fileMapProvider.Insert(fileMap);
        using (var stream = imageItem.Image.OpenReadStream())
        {
            await fileService.UploadFileAsync(stream, imageItem.Image.FileName);
        }

        return Ok(imageItem);
    }

    /// <summary>
    ///     Gets image specified by the reference id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{referenceId}")]
    public IActionResult Get(string referenceId)
    {
        byte[] bytes;
        var fileMap = fileMapProvider.GetById(referenceId);

        using (var stream = fileService.ReadFileAsync(fileMap.FilePath))
        {
            bytes = GetBytes(stream);
        }

        ;

        return File(bytes, "image/jpg");
    }

    private static byte[] GetBytes(Stream stream)
    {
        byte[] fileInbytes;
        var streamLength = (int) stream.Length; // total number of bytes read
        var numBytesReadPosition = 0; // actual number of bytes read

        fileInbytes = new byte[streamLength];

        while (streamLength > 0)
        {
            // Read may return anything from 0 to numBytesToRead.
            var n = stream.Read(fileInbytes, numBytesReadPosition, streamLength);
            // Break when the end of the file is reached.
            if (n == 0)
                break;
            numBytesReadPosition += n;
            streamLength -= n;
        }

        return fileInbytes;
    }
}