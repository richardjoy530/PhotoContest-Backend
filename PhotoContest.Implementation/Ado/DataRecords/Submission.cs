using System;

namespace PhotoContest.Implementation.Ado.DataRecords
{
    /// <summary>
    /// </summary>
    public class Submission : IDataRecord
    {
        /// <summary>
        /// </summary>
        public string Caption;

        /// <summary>
        /// </summary>
        public int ContestId;

        /// <summary>
        /// </summary>
        public int FileInfoId;

        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public string RefId;

        /// <summary>
        /// </summary>
        public DateTime UploadedOn;

        /// <summary>
        /// </summary>
        public int UserId;
    }
}