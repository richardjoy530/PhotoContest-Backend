using System;

namespace PhotoContest.Web.Contracts;

/// <summary>
/// Response class for UserInfo
/// </summary>
public class UserInfoResponse : UserInfoRequest
{
    /// <summary>
    /// Unique integer id representing a user 
    /// </summary>
    public int Id;

    /// <summary>
    /// Date on which the user was registered
    /// </summary>
    public DateTime RegistrationDate;
}