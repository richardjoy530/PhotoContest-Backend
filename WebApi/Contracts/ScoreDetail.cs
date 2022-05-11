namespace WebApi.Contracts
{
    /// <summary>
    /// Contains scores of a PhotoRntry
    /// </summary>
    public class ScoreDetail
    {
        /// <summary>
        /// ReferenceId
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Submission
        /// </summary>
        public Submission PhotoEntry { get; set; }

        /// <summary>
        /// Score of the referenced PhotoEnty
        /// </summary>
        public int Score { get; set; }
    }
}
