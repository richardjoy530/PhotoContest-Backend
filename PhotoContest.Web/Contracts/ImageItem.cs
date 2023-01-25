#region

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

#endregion

namespace PhotoContest.Web.Contracts;

/// <summary>
///     Contains uploaded image
/// </summary>
public class ImageItem
{
    /// <summary>
    ///     Unique Id assigned to this asset
    /// </summary>
    public string ReferenceId { get; set; }

    /// <summary>
    ///     The image file
    /// </summary>
    [Required]
    public IFormFile Image { get; set; }
}