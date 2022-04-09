using System;

namespace WebApi.Contracts
{
    /// <summary>
    /// Contains details of PhotoTheme
    /// </summary>
    public class PhotoTheme
    {
#nullable enable
        /// <summary>
        /// Unique id of the theme
        /// </summary>
        public string? ReferenceId { get; set; }
#nullable disable

        /// <summary>
        /// Theme of the photo contest
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Date of the contest
        /// </summary>
        public DateTime? ContestDate { get; set; }
    }
}
