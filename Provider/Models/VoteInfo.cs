namespace Provider.Models
{
    /// <summary>
    /// Contains voting details of a photographer
    /// </summary>
    public class VoteInfo : IDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        public VoteInfo(string referenceId)
        {
            Id = new Id { ReferenceId = referenceId };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integerId"></param>
        public VoteInfo(int integerId)
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
        public User Photographer { get; set; }

        /// <summary>
        /// Contest details of the vote
        /// </summary>
        public Contest Contest { get; set; }

        /// <summary>
        /// Id of the first voted <see cref="Submission"/>
        /// </summary>
        public Submission FirstPick { get; set; }

        /// <summary>
        /// Id of the second voted <see cref="Submission"/>
        /// </summary>
        public Submission SecondPick { get; set; }

        /// <summary>
        /// Id of the third voted <see cref="Submission"/>
        /// </summary>
        public Submission ThirdPick { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper)
        {
            FirstPick.ResolveIntegerId(mapper);
            SecondPick.ResolveIntegerId(mapper);
            ThirdPick.ResolveIntegerId(mapper);
            Photographer.ResolveIntegerId(mapper);
            Contest.ResolveIntegerId(mapper);
            Id.ResolveIntegerId(mapper);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.Submission)
        {
            FirstPick.ResolveReferenceId(mapper);
            SecondPick.ResolveReferenceId(mapper);
            ThirdPick.ResolveReferenceId(mapper);
            Photographer.ResolveReferenceId(mapper);
            Contest.ResolveReferenceId(mapper);
            Id.ResolveReferenceId(mapper, idType);
            IsResolved = true;
        }
    }
}
