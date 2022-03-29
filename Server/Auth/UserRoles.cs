using Server.Contracts;

namespace Server.Auth
{
    /// <summary>
    /// Static class containing available user roles
    /// </summary>
    public enum UserRoles
    {
        /// <summary>
        /// Admin role
        /// </summary>
        /// <remarks>Will have all access</remarks>
        Admin = 0x0111,

        /// <summary>
        /// User role
        /// </summary>
        /// <remarks>Will have View access to all data, Write access to User data</remarks>
        User = 0x0010,

        /// <summary>
        /// Host role
        /// </summary>
        /// <remarks>Will have all access to <see cref="PhotoTheme"/></remarks>
        Host = 0x0110,
    }
}
