namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public record ScoreInfo : IDataRecord
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