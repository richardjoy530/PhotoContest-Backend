namespace PhotoContest.Web.Contracts;

/// <summary>
///     Contains scores of a Submission
/// </summary>
public class ScoreDetail
{
    /// <summary>
    ///     ReferenceId
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Submission
    /// </summary>
    public int SubmissionId { get; set; }

    /// <summary>
    ///     Score of the referenced Submission
    /// </summary>
    public int Score { get; set; }
}