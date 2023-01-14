#region

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace WebApi.Auth;

/// <summary>
///     Authentication controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<IdentityUser> userManager;

    /// <summary>
    ///     Initializes new instance of AuthController
    /// </summary>
    /// <param name="_userManager"></param>
    /// <param name="_roleManager"></param>
    /// <param name="_configuration"></param>
    public AuthController(
        UserManager<IdentityUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        IConfiguration _configuration)
    {
        userManager = _userManager;
        roleManager = _roleManager;
        configuration = _configuration;
    }

    /// <summary>
    ///     Generated a JWT token if the given credentials are valid
    /// </summary>
    /// <remarks>Use this as a Bearer token in Authentication header of the HTTP requests</remarks>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("token")]
    public async Task<IActionResult> GetToken([FromBody] LoginRequest model)
    {
        var user = await userManager.FindByNameAsync(model.Username);
        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles) authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    /// <summary>
    ///     Registers a new user to the contest application
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        AuthResponse response;
        var existingUser = await userManager.FindByNameAsync(model.Username);
        if (existingUser != null)
        {
            response = new AuthResponse
            {
                Status = "Error",
                Message = "User already exists"
            };
            return StatusCode(StatusCodes.Status406NotAcceptable, response);
        }

        IdentityUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            response = new AuthResponse
            {
                Status = "Error",
                Message = string.Join(',', result.Errors.Select(e => e.Description))
            };
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        response = new AuthResponse
        {
            Status = "Success",
            Message = "User created successfully"
        };
        return Ok(response);
    }

    /// <summary>
    ///     Assigns <see cref="UserRoles" /> to a specified user
    /// </summary>
    /// <param name="assignRequest"></param>
    /// <returns>The caller must have <see cref="UserRoles.Admin" /></returns>
    [HttpPost]
    [Route("role")]
    public async Task<IActionResult> Assign([FromBody] AssignRoleRequest assignRequest)
    {
        AuthResponse response;
        var user = await userManager.FindByNameAsync(assignRequest.Username);
        if (user == null)
        {
            response = new AuthResponse
            {
                Status = "Error",
                Message = "User does not exists"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        await CreateRoles();
        await AssignRoles(user, assignRequest.Roles);

        response = new AuthResponse
        {
            Status = "Success",
            Message = "Role assigned successfully"
        };
        return Ok(response);
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"]));

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }

    // TODO: do this somewhere else. this is a one time runnable function throughout the lifetime of the application.
    private async Task CreateRoles()
    {
        if (!await roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
        if (!await roleManager.RoleExistsAsync(UserRoles.User.ToString()))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));
        if (!await roleManager.RoleExistsAsync(UserRoles.Host.ToString()))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Host.ToString()));
    }

    private async Task AssignRoles(IdentityUser user, UserRoles roles)
    {
        if ((roles & UserRoles.Admin) == UserRoles.Admin)
            await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
        if ((roles & UserRoles.User) == UserRoles.User)
            await userManager.AddToRoleAsync(user, UserRoles.User.ToString());
        if ((roles & UserRoles.Host) == UserRoles.Host)
            await userManager.AddToRoleAsync(user, UserRoles.Host.ToString());
    }
}