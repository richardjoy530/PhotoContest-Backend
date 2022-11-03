namespace PhotoContest.Models
{
    /// <summary>
    /// Contains voting details of a photographer
    /// </summary>
    public class PhotographerVoteDetails : IDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        public PhotographerVoteDetails(string referenceId)
        {
            Id = new Id { ReferenceId = referenceId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integerId"></param>
        public PhotographerVoteDetails(int integerId)
        {
            Id = new Id { IntegerId = integerId };
        }
        /// <summary>
        /// Id of the voting detail
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Details of the photographer whos voting details are encapsulated
        /// </summary>
        public Photographer Photographer { get; set; }

        /// <summary>
        /// Theme details of the vote
        /// </summary>
        public PhotoTheme Theme { get; set; }

        /// <summary>
        /// Id of the first voted <see cref="PhotoEntry"/>
        /// </summary>
        public PhotoEntry FirstVote { get; set; }

        /// <summary>
        /// Id of the second voted <see cref="PhotoEntry"/>
        /// </summary>
        public PhotoEntry SecondVote { get; set; }

        /// <summary>
        /// Id of the third voted <see cref="PhotoEntry"/>
        /// </summary>
        public PhotoEntry ThirdVote { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper)
        {
            FirstVote.ResolveIntegerId(mapper);
            SecondVote.ResolveIntegerId(mapper);
            ThirdVote.ResolveIntegerId(mapper);
            Photographer.ResolveIntegerId(mapper);
            Theme.ResolveIntegerId(mapper);
            Id.ResolveIntegerId(mapper);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.PhotoEntry)
        {
            FirstVote.ResolveReferenceId(mapper);
            SecondVote.ResolveReferenceId(mapper);
            ThirdVote.ResolveReferenceId(mapper);
            Photographer.ResolveReferenceId(mapper);
            Theme.ResolveReferenceId(mapper);
            Id.ResolveReferenceId(mapper, idType);
            IsResolved = true;
        }
    }
}
