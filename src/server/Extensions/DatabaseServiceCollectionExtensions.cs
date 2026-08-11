using CareerOS.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Extensions;

public static class DatabaseServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="CareerOSDbContext"/> against PostgreSQL. The
    /// connection string comes from configuration — User Secrets locally
    /// (see README/plan notes), an environment variable or Key Vault in
    /// other environments.
    /// </summary>
    public static IServiceCollection AddCareerOSDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CareerOS")
            ?? throw new InvalidOperationException(
                "Connection string 'CareerOS' is not configured. Set it via " +
                "'dotnet user-secrets set \"ConnectionStrings:CareerOS\" \"...\"' in src/server.");

        services.AddDbContext<CareerOSDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}
