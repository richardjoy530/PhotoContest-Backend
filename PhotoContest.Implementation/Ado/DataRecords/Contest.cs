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
    public string Theme;

    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
}