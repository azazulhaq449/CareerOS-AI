using CareerOS.Server.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace CareerOS.Server.Data.Seed;

/// <summary>
/// Seeds the single admin account this app expects — there is no
/// self-registration flow, so without this seeder nobody could ever log in.
/// Idempotent — checks for an existing user with the configured email first.
/// </summary>
public static class IdentityDataSeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        var email = configuration["AdminUser:Email"]
            ?? throw new InvalidOperationException("AdminUser:Email is not configured.");
        var password = configuration["AdminUser:Password"]
            ?? throw new InvalidOperationException("AdminUser:Password is not configured.");

        if (await userManager.FindByEmailAsync(email) is not null)
        {
            return;
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to seed admin user: {errors}");
        }
    }
}
