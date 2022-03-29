using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Auth
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes new instance of AuthController
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
        /// Generated a JWT token if the given credentials are valid
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
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

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
        /// 
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
                Message = "User created successfully",
            };
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest model)
        {
            AuthResponse response;
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                response = new AuthResponse
                {
                    Status = "Error",
                    Message = "User already exists",
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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

            await CreateRoles();
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
            await userManager.AddToRoleAsync(user, UserRoles.User);

            response = new AuthResponse
            {
                Status = "Success",
                Message = "User created successfully"
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
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Host))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Host));
            }
        }
    }
}
