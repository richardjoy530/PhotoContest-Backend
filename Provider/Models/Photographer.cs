namespace Provider.Models
{
    /// <summary>
    /// Contains details of Photographer
    /// </summary>
    public class Photographer : IDbModel
    {
        /// <summary>
        /// Id details of the Photographer record
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Name of the photographer
        /// </summary>
        public string UploaderName { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        /// <inheritdoc />
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType = IdType.Photographer)
        {
            Id.ResolveIntegerId(mapper, idType);
            IsResolved = true;
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.Photographer)
        {
            Id.ResolveReferenceId(mapper, idType);
            IsResolved = true;
        }
    }
}
