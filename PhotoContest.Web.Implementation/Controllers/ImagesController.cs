#region

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;
using PhotoContest.Web.Controllers;
using FileInfo = PhotoContest.Models.FileInfo;

#endregion

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
///     Image Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase, IImagesController
{
    private readonly IProvider<FileInfo> _fileMapProvider;
    private readonly IFileService _fileService;

    /// <summary>
    ///     Initializes new Image Controller
    /// </summary>
    /// <param name="fileService"></param>
    /// <param name="fileMapProvider"></param>
    public ImagesController(
        IFileService fileService,
        IProvider<FileInfo> fileMapProvider)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _fileMapProvider = fileMapProvider ?? throw new ArgumentNullException(nameof(fileMapProvider));
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

        var fileMap = new FileInfo(imageItem.ReferenceId) {Path = imageItem.Image.FileName};

        _fileMapProvider.Insert(fileMap);
        await using var stream = imageItem.Image.OpenReadStream();
        await _fileService.UploadFileAsync(stream, imageItem.Image.FileName);

        return Ok(imageItem);
    }

    /// <summary>
    ///     Gets image specified by the reference id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{referenceId}")]
    public IActionResult Get(string referenceId)
    {
        var fileMap = _fileMapProvider.GetById(referenceId);
        using var stream = _fileService.ReadFileAsync(fileMap.Path);
        var bytes = GetBytes(stream);
        return File(bytes, "image/jpg");
    }

    private static byte[] GetBytes(Stream stream)
    {
        var streamLength = (int) stream.Length; // total number of bytes read
        var numBytesReadPosition = 0; // actual number of bytes read
        var fileInBytes = new byte[streamLength];

        while (streamLength > 0)
        {
            // Read may return anything from 0 to numBytesToRead.
            var n = stream.Read(fileInBytes, numBytesReadPosition, streamLength);
            // Break when the end of the file is reached.
            if (n == 0)
                break;
            numBytesReadPosition += n;
            streamLength -= n;
        }

        return fileInBytes;
    }
}