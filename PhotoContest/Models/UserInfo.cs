#region

#endregion

using System;

namespace PhotoContest.Models;

/// <summary>
///     Contains details of UserInfo
/// </summary>
public class UserInfo
{
    /// <summary>
    ///     Id details of the <see cref="UserInfo" /> record
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Name of the userInfo
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// </summary>
    public string RefId { get; set; }

    /// <summary>
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// </summary>
    public DateTime RegisteredDate { get; set; }
}