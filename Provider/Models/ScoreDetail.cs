namespace Provider.Models
{
    /// <summary>
    /// Contains scores of a PhotoRntry
    /// </summary>
    public class ScoreDetail : IDbModel
    {
        /// <summary>
        /// Id of the score record
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Id of the PhotoEntry
        /// </summary>
        public PhotoEntry PhotoEntry { get; set; }

        /// <summary>
        /// Score of the referenced PhotoEnty
        /// </summary>
        public int Score { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType = IdType.ScoreDetail)
        {
            Id.ResolveIntegerId(mapper, idType);
            PhotoEntry.ResolveIntegerId(mapper);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.ScoreDetail)
        {
            Id.ResolveReferenceId(mapper, idType);
            PhotoEntry.ResolveReferenceId(mapper);
            IsResolved = true;
        }
    }
}
