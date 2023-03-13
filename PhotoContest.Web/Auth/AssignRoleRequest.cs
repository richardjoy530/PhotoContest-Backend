namespace PhotoContest.Web.Auth;

/// <summary>
///     Request to assign <see cref="UserRoles" /> to a specific user
/// </summary>
/// <remarks>User with <see cref="UserRoles.Admin" /> can only assign roles to other user</remarks>
public class AssignRoleRequest
{
    /// <summary>
    ///     Gets or Sets the roles for a given <see cref="Username" />
    /// </summary>
    public UserRoles Roles { get; set; }

    /// <summary>
    ///     Username to whom the <see cref="Roles" /> must be assigned to.
    /// </summary>
    public string Username { get; set; }
}