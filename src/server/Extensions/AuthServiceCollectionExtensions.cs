using System.Text;
using CareerOS.Server.Controllers;
using CareerOS.Server.Data;
using CareerOS.Server.Data.Entities;
using CareerOS.Server.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CareerOS.Server.Extensions;

public static class AuthServiceCollectionExtensions
{
    /// <summary>
    /// Registers ASP.NET Core Identity (user storage/password hashing) and
    /// JWT Bearer authentication. The bearer scheme accepts the token from
    /// either the standard <c>Authorization: Bearer</c> header (Postman,
    /// other API clients) or the httpOnly cookie the login endpoint sets
    /// (the React admin app, which never touches the token in JS) — whichever
    /// is present.
    /// </summary>
    public static IServiceCollection AddCareerOSAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<CareerOSDbContext>();

        var key = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is not configured.");
        var issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt:Issuer is not configured.");
        var audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt:Audience is not configured.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (string.IsNullOrEmpty(context.Token) &&
                            context.Request.Cookies.TryGetValue(AuthController.AuthCookieName, out var cookieToken))
                        {
                            context.Token = cookieToken;
                        }

                        return Task.CompletedTask;
                    },
                };
            });

        services.AddAuthorization();

        return services;
    }
}
