namespace PhotoContest.Models;

/// <summary>
///     Contains scores of a Submission
/// </summary>
public class ScoreInfo : IDbModel
{
    /// <summary>
    /// </summary>
    /// <param name="referenceId"></param>
    public ScoreInfo(string referenceId)
    {
        Id = new Id {ReferenceId = referenceId};
    }

    /// <summary>
    /// </summary>
    /// <param name="integerId"></param>
    public ScoreInfo(int integerId)
    {
        Id = new Id {IntegerId = integerId};
    }

    /// <summary>
    ///     Id of the score record
    /// </summary>
    public Id Id { get; set; }

    /// <summary>
    ///     Id of the Submission
    /// </summary>
    public Submission Submission { get; set; }

    /// <summary>
    ///     Score of the referenced Submission
    /// </summary>
    public int Score { get; set; }

    /// <inheritdoc />
    public bool IsResolved { get; set; }

    /// <inheritdoc />
    public void ResolveIntegerId(IReferenceIdMapper mapper)
    {
        Id.ResolveIntegerId(mapper);
        Submission.ResolveIntegerId(mapper);
        IsResolved = true;
    }

    /// <inheritdoc />
    public void ResolveReferenceId(IReferenceIdMapper mapper, IdType idType = IdType.ScoreDetail)
    {
        Id.ResolveReferenceId(mapper, idType);
        Submission.ResolveReferenceId(mapper);
        IsResolved = true;
    }
}