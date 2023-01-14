﻿#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace WebApi.Auth;

/// <summary>
///     LoginRequest
/// </summary>
public class LoginRequest
{
    /// <summary>
    ///     Gets or Sets the Username
    /// </summary>
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    /// <summary>
    ///     Gets or Sets thet Password
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}