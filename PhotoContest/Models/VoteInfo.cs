namespace PhotoContest.Models;

/// <summary>
///     Contains voting details of a userInfo
/// </summary>
public class VoteInfo : IDbModel
{
    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    public VoteInfo(string referenceId)
    {
        Id = new Id {ReferenceId = referenceId};
    }

    /// <summary>
    /// </summary>
    /// <param name="integerId"></param>
    public VoteInfo(int integerId)
    {
        Id = new Id {IntegerId = integerId};
    }

    /// <summary>
    ///     Id of the voting detail
    /// </summary>
    public Id Id { get; set; }

    /// <summary>
    ///     Details of the userInfo who's voting details are encapsulated
    /// </summary>
    public UserInfo UserInfo { get; set; }

    /// <summary>
    ///     Contest details of the vote
    /// </summary>
    public Contest Contest { get; set; }

    /// <summary>
    ///     Id of the first voted <see cref="Submission" />
    /// </summary>
    public Submission FirstSubmission { get; set; }

    /// <summary>
    ///     Id of the second voted <see cref="Submission" />
    /// </summary>
    public Submission SecondSubmission { get; set; }

    /// <summary>
    ///     Id of the third voted <see cref="Submission" />
    /// </summary>
    public Submission ThirdSubmission { get; set; }

    /// <inheritdoc />
    public bool IsResolved { get; set; }

    /// <inheritdoc />
    public void ResolveIntegerId(IReferenceIdMapper mapper)
    {
        FirstSubmission.ResolveIntegerId(mapper);
        SecondSubmission.ResolveIntegerId(mapper);
        ThirdSubmission.ResolveIntegerId(mapper);
        UserInfo.ResolveIntegerId(mapper);
        Contest.ResolveIntegerId(mapper);
        Id.ResolveIntegerId(mapper);
        IsResolved = true;
    }

    /// <inheritdoc />
    public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.PhotoEntry)
    {
        FirstSubmission.ResolveReferenceId(mapper);
        SecondSubmission.ResolveReferenceId(mapper);
        ThirdSubmission.ResolveReferenceId(mapper);
        UserInfo.ResolveReferenceId(mapper);
        Contest.ResolveReferenceId(mapper);
        Id.ResolveReferenceId(mapper, idType);
        IsResolved = true;
    }
}