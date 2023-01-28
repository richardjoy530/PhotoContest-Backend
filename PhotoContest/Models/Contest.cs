#region

using System;
using System.Data;

#endregion

namespace PhotoContest.Models;

/// <summary>
///     Contains details of Contest
/// </summary>
public class Contest : IDbModel
{
    /// <summary>
    /// </summary>
    public Contest(string referenceId)
    {
        Id = new Id {ReferenceId = referenceId};
    }

    /// <summary>
    /// </summary>
    /// <param name="integerId"></param>
    public Contest(int integerId)
    {
        Id = new Id {IntegerId = integerId};
    }

    /// <summary>
    /// </summary>
    /// <param name="dataRecord"></param>
    public Contest(IDataRecord dataRecord)
    {
        Id = new Id {IntegerId = (int) dataRecord["Id"]};
        Theme = (string) dataRecord["Contest"];
        EndDate = (DateTime) dataRecord["EndDate"];
    }

    /// <summary>
    ///     Id details of the <see cref="Contest" /> record
    /// </summary>
    public Id Id { get; set; }

    /// <summary>
    ///     Contest of the photo contest
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    ///     Date of the contest
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <inheritdoc />
    public bool IsResolved { get; set; }

    /// <inheritdoc />
    public void ResolveIntegerId(IReferenceIdMapper mapper)
    {
        Id.IntegerId = mapper.GetIntegerId(Id.ReferenceId);
        IsResolved = true;
    }

    /// <inheritdoc />
    public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.Theme)
    {
        Id.ReferenceId = mapper.GetReferenceId(Id.IntegerId, idType);
        IsResolved = true;
    }
}