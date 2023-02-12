namespace PhotoContest.Web.Contracts;

/// <summary>
///     Contains voting details of a photographer
/// </summary>
public class VoteInfo
{
    /// <summary>
    ///     Unique id of the vote details
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Active theme during the voting
    /// </summary>
    public int ContestId { get; set; }

    /// <summary>
    ///     Reference Id of the first voted <see cref="Submission" />
    /// </summary>
    public int FirstVoteId { get; set; }

    /// <summary>
    ///     Reference Id of the second voted <see cref="Submission" />
    /// </summary>
    public int SecondVoteId { get; set; }

    /// <summary>
    ///     Reference Id of the third voted <see cref="Submission" />
    /// </summary>
    public int ThirdVoteId { get; set; }
}