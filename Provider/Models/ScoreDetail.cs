namespace Provider.Models
{
    /// <summary>
    /// Contains scores of a PhotoRntry
    /// </summary>
    public class ScoreDetail : IResolvable
    {
        /// <summary>
        /// Id of the PhotoEntry
        /// </summary>
        public Id EntryId { get; set; }

        /// <summary>
        /// Score of the referenced PhotoEnty
        /// </summary>
        public int Score { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType = IdType.ScoreDetail)
        {
            EntryId.ResolveIntegerId(mapper, idType);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.ScoreDetail)
        {
            EntryId.ResolveReferenceId(mapper, idType);
            IsResolved = true;
        }
    }
}
