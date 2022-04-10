using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts
{
    /// <summary>
    /// Contains uploaded image
    /// </summary>
    public class ImageItem
    {
        /// <summary>
        /// Unique Id assigned to this asset
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// The image file
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }
    }
}
