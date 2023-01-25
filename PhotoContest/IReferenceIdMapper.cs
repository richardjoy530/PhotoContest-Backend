#region

using PhotoContest.Models;

#endregion

namespace PhotoContest;

/// <summary>
///     An interface for Data access of integer id and reference id from/into [dbo].[IdMap]
/// </summary>
public interface IReferenceIdMapper
{
    /// <summary>
    ///     Maps the reference Id to its corresponding integer ID in the Database
    /// </summary>
    /// <param name="referenceId"></param>
    /// <returns>Integer Id</returns>
    public int GetIntegerId(string referenceId);

    /// <summary>
    ///     Maps the integer ID to its corresponding publicly visible reference Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="idType"></param>
    /// <returns>String representation of the GUID</returns>
    public string GetReferenceId(int id, IdType idType);

    /// <summary>
    ///     Inserts a map record into [dbo].[IdMap]
    /// </summary>
    /// <param name="id"></param>
    /// <param name="idType"></param>
    public void InsertIdMap(Id id, IdType idType);
}