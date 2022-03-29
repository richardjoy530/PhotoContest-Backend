using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Server.Contracts
{
    /// <summary>
    /// Contains uploaded image
    /// </summary>
    public class ImageItem
    {
        /// <summary>
        /// The image file
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }
    }
}
