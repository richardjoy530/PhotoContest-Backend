namespace PhotoContest.Models;

/// <summary>
///     Contains scores of a Submission
/// </summary>
public class ScoreInfo
{
    /// <summary>
    ///     Id of the score record
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Id of the Submission
    /// </summary>
    public Submission Submission { get; set; }

    /// <summary>
    ///     Score of the referenced Submission
    /// </summary>
    public int Score { get; set; }
}