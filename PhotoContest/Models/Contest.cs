#region

using System;

#endregion

namespace PhotoContest.Models;

/// <summary>
///     Contains details of Contest
/// </summary>
public class Contest
{
    /// <summary>
    ///     Id details of the <see cref="Contest" /> record
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Contest of the photo contest
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    ///     Date of the contest
    /// </summary>
    public DateTime EndDate { get; set; }
}