namespace Provider.Models
{
    /// <summary>
    /// Contains details of Photographer
    /// </summary>
    public class Photographer : IResolvable
    {
        /// <summary>
        /// Reference Id of the phographer
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Name of the photographer
        /// </summary>
        public string UploaderName { get; set; }

        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public void ResolveId(IReferenceIdMapper mapper)
        {
            Id = mapper.GetIntegerId(ReferenceId, IdType.PhotoEntry);
        }

        /// <inheritdoc />
        public void ResolveReferenceId(IReferenceIdMapper mapper)
        {
            ReferenceId = mapper.GetReferenceId(Id, IdType.PhotoEntry);
        }
    }
}
