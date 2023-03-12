namespace PhotoContest.Implementation.Ado.DataRecords;

/// <summary>
/// </summary>
public record FileInfo : IDataRecord
{
    /// <summary>
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// </summary>
    public string Path;
}