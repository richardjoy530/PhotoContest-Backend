#region

using System;

#endregion

namespace PhotoContest.Web.Auth;

/// <summary>
///     Static class containing available user roles
/// </summary>
[Flags]
public enum UserRoles
{
    /// <summary>
    ///     Admin role
    /// </summary>
    /// <remarks>Will have all access</remarks>
    Admin = 0b0111,

    /// <summary>
    ///     User role
    /// </summary>
    /// <remarks>Will have View access to all data, Write access to User data</remarks>
    User = 0b0010,

    /// <summary>
    ///     Host role
    /// </summary>
    /// <remarks>Will have all access to Contest</remarks>
    Host = 0b0110
}