using Provider.Models;

namespace Provider
{
    /// <summary>
    /// An interface for Dataaccess of interger id and reference id from/into [dbo].[IdMap]
    /// </summary>
    public interface IReferenceIdProvider : IReferenceIdMapper
    {
        /// <summary>
        /// Inserts a map record into [dbo].[IdMap]
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idType"></param>
        public void InsertIdMap(Id id, IdType idType);
    }
}
