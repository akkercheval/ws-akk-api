using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AkkWebService.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AkkWebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly AdminUserSettings _adminUser;

    public AuthController(IOptions<JwtSettings> jwtSettings, IOptions<AdminUserSettings> adminUser)
    {
        _jwtSettings = jwtSettings.Value;
        _adminUser = adminUser.Value;
    }

    /// <summary>
    /// Authenticate and receive a JWT token.
    /// </summary>
    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Validate username (case-insensitive)
        if (!string.Equals(request.Username, _adminUser.Username, StringComparison.OrdinalIgnoreCase))
            return Unauthorized(new { message = "Invalid username or password." });

        // Validate password against stored hash
        if (!BCrypt.Net.BCrypt.Verify(request.Password, _adminUser.PasswordHash))
            return Unauthorized(new { message = "Invalid username or password." });

        // Generate JWT
        var token = GenerateToken(request.Username);
        var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        return Ok(new LoginResponseDto
        {
            Token = token,
            ExpiresAt = expiration
        });
    }

    private string GenerateToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
