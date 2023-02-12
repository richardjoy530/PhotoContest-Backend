#region

using System;

#endregion

namespace PhotoContest.Web.Contracts;

/// <summary>
///     Contains details of Contest
/// </summary>
public class Contest
{
    /// <summary>
    ///     Contest of the photo contest
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    ///     End date of the contest
    /// </summary>
    public DateTime EndDate { get; set; }
}