namespace PhotoContest.Implementation.Ado.DataRecords;

/// <summary>
/// </summary>
public record ScoreInfo : IDataRecord
{
    /// <summary>
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// </summary>
    public int Score;

    /// <summary>
    /// </summary>
    public int SubmissionId;
}