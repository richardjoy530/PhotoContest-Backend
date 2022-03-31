namespace Provider.Models
{
    /// <summary>
    /// Contains voting details of a photographer
    /// </summary>
    public class PhotographerVoteDetails : IDbModel
    {
        /// <summary>
        /// Details of the photographer whos voting details are encapsulated
        /// </summary>
        public Photographer Photographer { get; set; }

        /// <summary>
        /// Id of the first voted <see cref="PhotoEntry"/>
        /// </summary>
        public Id FirstVoteId { get; set; }

        /// <summary>
        /// Id of the second voted <see cref="PhotoEntry"/>
        /// </summary>
        public Id SecondVoteId { get; set; }

        /// <summary>
        /// Id of the third voted <see cref="PhotoEntry"/>
        /// </summary>
        public Id ThirdVoteId { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType = IdType.PhotoEntry)
        {
            FirstVoteId.ResolveIntegerId(mapper, idType);
            SecondVoteId.ResolveIntegerId(mapper, idType);
            ThirdVoteId.ResolveIntegerId(mapper, idType);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.PhotoEntry)
        {
            FirstVoteId.ResolveReferenceId(mapper, idType);
            SecondVoteId.ResolveReferenceId(mapper, idType);
            ThirdVoteId.ResolveReferenceId(mapper, idType);
            IsResolved = true;
        }
    }
}
