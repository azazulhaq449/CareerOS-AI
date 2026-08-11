using CareerOS.Server.Data.Entities;

namespace CareerOS.Server.Services.Auth;

public record JwtToken(string AccessToken, DateTime ExpiresAtUtc);

public interface IJwtTokenService
{
    JwtToken CreateToken(ApplicationUser user);
}
