namespace WebApi.Contracts
{
    /// <summary>
    /// Contains voting details of a photographer
    /// </summary>
    public class VoteInfo
    {
        /// <summary>
        /// Unique id of the vote details
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Active theme during the voting
        /// </summary>
        public Contest Theme { get; set; }

        /// <summary>
        /// Details of the photographer whos voting details are encapsulated
        /// </summary>
        public User Photographer { get; set; }

        /// <summary>
        /// Reference Id of the first voted <see cref="Submission"/>
        /// </summary>
        public Submission FirstPick { get; set; }

        /// <summary>
        /// Reference Id of the second voted <see cref="Submission"/>
        /// </summary>
        public Submission SecondPick { get; set; }

        /// <summary>
        /// Reference Id of the third voted <see cref="Submission"/>
        /// </summary>
        public Submission ThirdPick { get; set; }
    }
}
