namespace PhotoContest;

/// <summary>
///     Type of the asset for id look up
/// </summary>
public enum IdType
{
    /// <summary>
    ///     Submission
    /// </summary>
    PhotoEntry,

    /// <summary>
    ///     UserInfo
    /// </summary>
    Photographer,

    /// <summary>
    ///     ScoreInfo
    /// </summary>
    ScoreDetail,

    /// <summary>
    ///     File
    /// </summary>
    File,

    /// <summary>
    ///     Contest
    /// </summary>
    Theme,

    /// <summary>
    ///     VotingDetail
    /// </summary>
    VotingDetail
}