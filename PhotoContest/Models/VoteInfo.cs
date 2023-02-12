namespace PhotoContest.Models;

/// <summary>
///     Contains voting details of a userInfo
/// </summary>
public class VoteInfo
{
    /// <summary>
    ///     Id of the voting detail
    /// </summary>
    public int Id { get; set; }

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
}