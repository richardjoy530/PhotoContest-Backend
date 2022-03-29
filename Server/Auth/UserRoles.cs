using Server.Contracts;

namespace Server.Auth
{
    /// <summary>
    /// Static class containing available user roles
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// Admin role
        /// </summary>
        /// <remarks>Will have all access</remarks>
        public const string Admin = "Admin";

        /// <summary>
        /// User role
        /// </summary>
        /// <remarks>Will have View access to all data, Write access to User data</remarks>
        public const string User = "User";

        /// <summary>
        /// Host role
        /// </summary>
        /// <remarks>Will have all access to <see cref="PhotoTheme"/></remarks>
        public const string Host = "Host";
    }
}
