using System;

namespace PhotoContest.Models
{
    /// <summary>
    ///     Contains details of Contest
    /// </summary>
    public class Contest
    {
        /// <summary>
        ///     Id details of the <see cref="Contest" /> record
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        ///     Contest of the photo contest
        /// </summary>
        public string Theme { get; init; }

        /// <summary>
        ///     Date of the contest
        /// </summary>
        public DateTime EndDate { get; init; }
    }
}