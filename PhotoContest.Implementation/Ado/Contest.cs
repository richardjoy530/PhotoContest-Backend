using System;

namespace PhotoContest.Implementation.Ado;

/// <summary>
/// 
/// </summary>
public record Contest : IRecord
{
    /// <summary>
    /// 
    /// </summary>
    public int Id;

    /// <summary>
    /// 
    /// </summary>
    public string Theme;

    /// <summary>
    /// 
    /// </summary>
    public DateTime EndDate;
}