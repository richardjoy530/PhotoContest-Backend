namespace Server.Contracts
{
    public class UserVoteDetails
    {
        /// <summary>
        /// Details of the photographer whos voting details are encapsulated
        /// </summary>
        public Photographer Photographer { get; set; }

        /// <summary>
        /// Reference Id of the first voted <see cref="PhotoEntry"/>
        /// </summary>
        public string FirstVoteId { get; set; }

        /// <summary>
        /// Reference Id of the second voted <see cref="PhotoEntry"/>
        /// </summary>
        public string SecondVoteId { get; set; }

        /// <summary>
        /// Reference Id of the third voted <see cref="PhotoEntry"/>
        /// </summary>
        public string ThirdVoteId { get; set; }
    }
}
