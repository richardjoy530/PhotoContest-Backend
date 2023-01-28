#region

using System.Data;

#endregion

namespace PhotoContest.Models;

/// <summary>
///     Contains details of UserInfo
/// </summary>
public class UserInfo : IDbModel
{
    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    public UserInfo(string referenceId)
    {
        Id = new Id {ReferenceId = referenceId};
    }

    /// <summary>
    /// </summary>
    /// <param name="integerId"></param>
    public UserInfo(int integerId)
    {
        Id = new Id {IntegerId = integerId};
    }

    /// <summary>
    /// </summary>
    public UserInfo(IDataRecord dataRecord)
    {
        Id = new Id {IntegerId = (int) dataRecord["Id"]};
        Name = (string) dataRecord["Name"];
    }

    /// <summary>
    ///     Id details of the <see cref="UserInfo" /> record
    /// </summary>
    public Id Id { get; set; }

    /// <summary>
    ///     Name of the userInfo
    /// </summary>
    public string Name { get; set; }

    /// <inheritdoc />
    public bool IsResolved { get; set; }

    /// <inheritdoc />
    public void ResolveIntegerId(IReferenceIdMapper mapper)
    {
        Id.ResolveIntegerId(mapper);
        IsResolved = true;
    }

    /// <inheritdoc />
    public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.Photographer)
    {
        Id.ResolveReferenceId(mapper, idType);
        IsResolved = true;
    }
}