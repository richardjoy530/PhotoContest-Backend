namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public record ScoreInfo : IRecord
{
    /// <summary>
    /// 
    /// </summary>
    public int Id;

    /// <summary>
    /// 
    /// </summary>
    public int SubmissionId;

    /// <summary>
    /// 
    /// </summary>
    public int Score;
}