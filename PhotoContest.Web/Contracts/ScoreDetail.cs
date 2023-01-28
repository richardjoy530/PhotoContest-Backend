namespace PhotoContest.Web.Contracts;

/// <summary>
///     Contains scores of a Submission
/// </summary>
public class ScoreDetail
{
    /// <summary>
    ///     ReferenceId
    /// </summary>
    public string ReferenceId { get; set; }

    /// <summary>
    ///     Submission
    /// </summary>
    public Submission Submission { get; set; }

    /// <summary>
    ///     Score of the referenced PhotoEnty
    /// </summary>
    public int Score { get; set; }
}