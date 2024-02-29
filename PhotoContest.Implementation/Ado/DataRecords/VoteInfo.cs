namespace PhotoContest.Implementation.Ado.DataRecords
{
    /// <summary>
    /// </summary>
    public class VoteInfo : IDataRecord
    {
        /// <summary>
        /// </summary>
        public int ContestId;

        /// <summary>
        /// </summary>
        public int FirstId;

        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public int SecondId;

        /// <summary>
        /// </summary>
        public int ThirdId;

        /// <summary>
        /// </summary>
        public int UserId;
    }
}