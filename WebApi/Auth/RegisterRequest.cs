#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace WebApi.Auth;

/// <summary>
///     RegisterRequest
/// </summary>
public class RegisterRequest
{
    /// <summary>
    ///     Gets or Sets the Username
    /// </summary>
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    /// <summary>
    ///     Gets or Sets the Email Id
    /// </summary>
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    /// <summary>
    ///     Gets or Sets the password
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}