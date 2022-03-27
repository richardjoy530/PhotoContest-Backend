namespace Provider
{
    /// <summary>
    /// An interface for referenceId <=> integerId mapper
    /// </summary>
    public interface IReferenceIdMapper
    {
        /// <summary>
        /// Maps the reference Id to its corresponding integer ID in the Database
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="idType"></param>
        /// <returns>Integer Id</returns>
        public int GetIntegerId(string referenceId, IdType idType);

        /// <summary>
        /// Maps the integer ID to its corresponding publically visible reference Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idType"></param>
        /// <returns>String reperesentation of the GUID</returns>
        public string GetReferenceId(int id, IdType idType);
    }
}
