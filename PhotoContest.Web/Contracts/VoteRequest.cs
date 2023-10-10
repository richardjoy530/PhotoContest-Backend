namespace PhotoContest.Web.Contracts
{
    /// <summary>
    /// Contains voting details of a user for a contest
    /// </summary>
    public class VoteRequest
    {
        //todo get this id from the current contest
        /// <summary>
        /// Active theme during the voting
        /// </summary>
        public int ContestId { get; set; }

        /// <summary>
        /// Id of the first voted Submission. Keep this '0' if not voted
        /// </summary>
        public int FirstVoteId { get; set; }

        /// <summary>
        /// Id of the second voted Submission. Keep this '0' if not voted
        /// </summary>
        public int SecondVoteId { get; set; }

        /// <summary>
        /// Id of the third voted Submission. Keep this '0' if not voted
        /// </summary>
        public int ThirdVoteId { get; set; }
    }
}