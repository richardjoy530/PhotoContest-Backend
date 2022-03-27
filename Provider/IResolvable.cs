namespace Provider
{
    /// <summary>
    /// An interface for resolving 
    /// </summary>
    public interface IResolvable
    {
        /// <summary>
        /// Database integer Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Resolve the integer id of the calling object
        /// </summary>
        /// <param name="mapper"></param>
        public void ResolveId(IReferenceIdMapper mapper);

        /// <summary>
        /// Resolve the reference id of the calling object
        /// </summary>
        /// <param name="mapper"></param>
        public void ResolveReferenceId(IReferenceIdMapper mapper);
    }
}
