#region

using System;

#endregion

namespace PhotoContest.Web.Contracts;

/// <summary>
///     Contains details of Contest
/// </summary>
public class Contest
{
#nullable enable
    /// <summary>
    ///     Unique id of the theme
    /// </summary>
    public string? ReferenceId { get; set; }
#nullable disable

    /// <summary>
    ///     Contest of the photo contest
    /// </summary>
    public string Theme { get; set; }

    /// <summary>
    ///     Date of the contest
    /// </summary>
    public DateTime? ContestDate { get; set; }
}