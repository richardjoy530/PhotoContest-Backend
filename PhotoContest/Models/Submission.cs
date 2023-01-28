#region

using System;
using System.Data;

#endregion

namespace PhotoContest.Models;

/// <summary>
///     Contains the details of Submission
/// </summary>
public class Submission : IDbModel
{
    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    public Submission(string referenceId)
    {
        Id = new Id {ReferenceId = referenceId};
    }

    /// <summary>
    /// </summary>
    /// <param name="integerId"></param>
    public Submission(int integerId)
    {
        Id = new Id {IntegerId = integerId};
    }

    /// <summary>
    /// </summary>
    /// <param name="dataRecord"></param>
    public Submission(IDataRecord dataRecord)
    {
        Id = new Id {IntegerId = (int) dataRecord["Id"]};
        Contest = new Contest((int) dataRecord["Id"]);
        FileInfo = new Id {IntegerId = (int) dataRecord["FileInfo"]};
        Caption = (string) dataRecord["Caption"];
        // TODO: Change this to go fetch the userInfo from cache.
        UserInfo = new UserInfo((int) dataRecord["PhotographerId"]);
        UploadedOn = (DateTime) dataRecord["UploadedOn"];
    }

    /// <summary>
    ///     Id details of the Submission record
    /// </summary>
    public Id Id { get; set; }

    /// <summary>
    ///     Contest of the photo. This must correspond to <see cref="Models.Contest" />
    /// </summary>
    public Contest Contest { get; set; }

    /// <summary>
    ///     Details of the userInfo who uploaded the photo
    /// </summary>
    public UserInfo UserInfo { get; set; }

    /// <summary>
    ///     Id details of the associated photo. This should always be valid.
    /// </summary>
    public Id FileInfo { get; set; }

    /// <summary>
    ///     A creative caption to go in with your photo
    /// </summary>
    public string Caption { get; set; }

    /// <summary>
    ///     Date and time of the upload
    /// </summary>
    public DateTime UploadedOn { get; set; }

    /// <inheritdoc />
    public bool IsResolved { get; set; }

    /// <inheritdoc />
    public void ResolveIntegerId(IReferenceIdMapper mapper)
    {
        Id.ResolveIntegerId(mapper);
        Contest.ResolveIntegerId(mapper);
        FileInfo.ResolveIntegerId(mapper);
        IsResolved = true;
    }

    /// <inheritdoc />
    public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.PhotoEntry)
    {
        Id.ResolveReferenceId(mapper, idType);
        Contest.ResolveReferenceId(mapper);
        FileInfo.ResolveReferenceId(mapper, IdType.File);
        IsResolved = true;
    }
}