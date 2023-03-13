#region

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoContest.Web.Contracts;

#endregion

namespace PhotoContest.Web.Implementation.Controllers;

/// <summary>
/// Image Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase, IFileController
{
    private readonly IFileService _fileService;

    /// <summary>
    ///     Initializes new Image Controller
    /// </summary>
    /// <param name="fileService"></param>
    public FileController(IFileService fileService)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    /// <summary>
    ///     Api to save an image
    /// </summary>
    /// <param name="imageFileRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SaveImage([FromForm] ImageFileRequest imageFileRequest)
    {
        await _fileService.SaveFile(imageFileRequest.Image);
        return Ok(imageFileRequest);
    }

    /// <summary>
    ///     Gets image specified by the reference id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        using var stream = _fileService.GetFile(id);
        var bytes = GetBytes(stream);
        return File(bytes, "image/jpg");
    }

    private static byte[] GetBytes(Stream stream)
    {
        var streamLength = (int)stream.Length; // total number of bytes read
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