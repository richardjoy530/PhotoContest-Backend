using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Server.Contracts
{
    /// <summary>
    /// Contains uploaded image
    /// </summary>
    public class ImageItem
    {
#nullable enable
        /// <summary>
        /// Reference id of the image
        /// </summary>
        public string? ReferenceId { get; set; }
#nullable disable

        /// <summary>
        /// The image file
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }
    }
}
