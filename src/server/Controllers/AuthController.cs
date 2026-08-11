using CareerOS.Server.Data.Entities;
using CareerOS.Server.Models.Auth;
using CareerOS.Server.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService,
    IWebHostEnvironment environment) : ControllerBase
{
    public const string AuthCookieName = "careeros_access_token";

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }

        var token = jwtTokenService.CreateToken(user);

        // Secure requires HTTPS — local dev runs plain HTTP, so a Secure
        // cookie would be silently dropped by the browser and login would
        // appear to succeed while never actually persisting a session.
        Response.Cookies.Append(AuthCookieName, token.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = !environment.IsDevelopment(),
            SameSite = SameSiteMode.Strict,
            Expires = token.ExpiresAtUtc,
        });

        return Ok(new LoginResponse { AccessToken = token.AccessToken, ExpiresAtUtc = token.ExpiresAtUtc });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(AuthCookieName);
        return NoContent();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<CurrentUserResponse>> Me()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }

        return Ok(new CurrentUserResponse { Email = user.Email ?? string.Empty });
    }
}
