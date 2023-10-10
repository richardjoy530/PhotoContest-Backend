#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace PhotoContest.Web.Contracts
{
    /// <summary>
    ///     Contains details of Contest
    /// </summary>
    public class ContestRequest
    {
        /// <summary>
        ///     Contest of the photo contest
        /// </summary>
        [Required]
        public string Theme { get; set; }

        /// <summary>
        ///     End date of the contest in yyyy-mm-dd format timezone IST
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }
    }
}