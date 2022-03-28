namespace Provider
{
    /// <summary>
    /// An interface for resolving 
    /// </summary>
    public interface IResolvable
    {
        /// <summary>
        /// Indicates if the Id is resolved
        /// </summary>
        public bool IsResolved { get; set; }

        /// <summary>
        /// Resolve the integer id of the calling object
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="idType"></param>
        public void ResolveIntegerId(IReferenceIdMapper mapper, IdType idType);

        /// <summary>
        /// Resolve the reference id of the calling object
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="idType"></param>
        public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType);
    }
}
