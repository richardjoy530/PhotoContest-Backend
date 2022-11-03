namespace PhotoContest.Models
{
    /// <summary>
    /// Class encapsulates ids
    /// </summary>
    public class Id : IResolvable
    {
        /// <summary>
        /// Integer id of the record in the database
        /// </summary>
        public int IntegerId { get; set; }

        /// <summary>
        /// Publicaly available uinque id (GUID format)
        /// </summary>
        public string ReferenceId { get; set; }

        /// <inheritdoc />
        public bool IsResolved { get; set; }

        ///<inheritdoc/>
        public void ResolveIntegerId(IReferenceIdMapper mapper)
        {
            IntegerId = mapper.GetIntegerId(ReferenceId);
            IsResolved = true;
        }

        ///<inheritdoc/>
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType)
        {
            ReferenceId = mapper.GetReferenceId(IntegerId, idType);
            IsResolved = true;
        }
    }
}
