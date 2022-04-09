namespace WebApi.Contracts
{
    /// <summary>
    /// Contains voting details of a photographer
    /// </summary>
    public class PhotographerVoteDetails
    {
        /// <summary>
        /// Unique id of the vote details
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Active theme during the voting
        /// </summary>
        public PhotoTheme Theme { get; set; }

        /// <summary>
        /// Details of the photographer whos voting details are encapsulated
        /// </summary>
        public Photographer Photographer { get; set; }

        /// <summary>
        /// Reference Id of the first voted <see cref="PhotoEntry"/>
        /// </summary>
        public PhotoEntry FirstVote { get; set; }

        /// <summary>
        /// Reference Id of the second voted <see cref="PhotoEntry"/>
        /// </summary>
        public PhotoEntry SecondVote { get; set; }

        /// <summary>
        /// Reference Id of the third voted <see cref="PhotoEntry"/>
        /// </summary>
        public PhotoEntry ThirdVote { get; set; }
    }
}
