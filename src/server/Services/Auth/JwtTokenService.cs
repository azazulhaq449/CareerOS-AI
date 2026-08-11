using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CareerOS.Server.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CareerOS.Server.Services.Auth;

/// <summary>
/// Hand-issues signed JWTs rather than relying on ASP.NET Core Identity's
/// default bearer-token scheme (which issues opaque, data-protection-backed
/// tokens, not standards-compliant JWTs) — this project needs real,
/// inspectable JWTs for the Postman workflow.
/// </summary>
public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

    public JwtToken CreateToken(ApplicationUser user)
    {
        var key = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is not configured.");
        var issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt:Issuer is not configured.");
        var audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt:Audience is not configured.");

        var expiresAtUtc = DateTime.UtcNow.Add(TokenLifetime);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: signingCredentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new JwtToken(accessToken, expiresAtUtc);
    }
}
