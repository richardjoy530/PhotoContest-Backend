using System;

namespace PhotoContest.Models
{
    /// <summary>
    ///     Contains the details of Submission
    /// </summary>
    public class Submission
    {
        /// <summary>
        ///     Id details of the Submission record
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public string RefId { get; init; }

        /// <summary>
        ///     Contest of the photo. This must correspond to <see cref="Models.Contest" />
        /// </summary>
        public Contest Contest { get; init; }

        /// <summary>
        ///     Details of the userInfo who uploaded the photo
        /// </summary>
        public UserInfo UserInfo { get; init; }

        /// <summary>
        ///     Id details of the associated photo. This should always be valid.
        /// </summary>
        public FileInfo FileInfo { get; init; }

        /// <summary>
        ///     A creative caption to go in with your photo
        /// </summary>
        public string Caption { get; init; }

        /// <summary>
        ///     Date and time of the upload
        /// </summary>
        public DateTime UploadedOn { get; init; }
    }
}