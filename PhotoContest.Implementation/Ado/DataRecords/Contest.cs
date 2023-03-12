using System;

namespace PhotoContest.Implementation.Ado.DataRecords;

/// <summary>
/// </summary>
public record Contest : IDataRecord
{
    /// <summary>
    /// </summary>
    public DateTime EndDate;

    /// <summary>
    /// </summary>
    public int Id;

    /// <summary>
    /// </summary>
    public string Theme;
}