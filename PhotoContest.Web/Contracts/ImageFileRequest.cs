using Microsoft.AspNetCore.Http;

namespace PhotoContest.Web.Contracts;

/// <summary>
/// Wrapper class for the IFormFile
/// </summary>
public class ImageFileRequest
{
    /// <summary>
    /// The image file
    /// </summary>
    public IFormFile Image;
}