#region

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#endregion

namespace PhotoContest.Web.Auth;

/// <summary>
///     A helper for the data access of Microsoft.AspNetCore.Identity
/// </summary>
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    /// <summary>
    ///     Initializes a new ApplicationDbContext class
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}