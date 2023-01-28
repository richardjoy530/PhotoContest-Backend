#region

using System;
using System.Data;

#endregion

namespace PhotoContest.Models;

/// <summary>
///     FileInfo
/// </summary>
public class FileInfo : IDbModel
{
    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    public FileInfo(string referenceId)
    {
        Id = new Id {ReferenceId = referenceId};
    }

    /// <summary>
    /// </summary>
    /// <param name="integerId"></param>
    public FileInfo(int integerId)
    {
        Id = new Id {IntegerId = integerId};
    }

    /// <summary>
    /// </summary>
    /// <param name="dataRecord"></param>
    public FileInfo(IDataRecord dataRecord)
    {
        Id = new Id {IntegerId = (int) dataRecord["Id"]};
        Path = (string) dataRecord["Path"];
    }

    /// <summary>
    ///     Id
    /// </summary>
    public Id Id { get; set; }

    /// <summary>
    ///     Path of the location where the file is saved
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// </summary>
    public bool IsResolved
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void ResolveIntegerId(IReferenceIdMapper mapper)
    {
        Id.IntegerId = mapper.GetIntegerId(Id.ReferenceId);
        IsResolved = true;
    }

    /// <inheritdoc />
    public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.File)
    {
        Id.ReferenceId = mapper.GetReferenceId(Id.IntegerId, idType);
        IsResolved = true;
    }
}