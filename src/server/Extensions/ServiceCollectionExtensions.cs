using CareerOS.Server.Repositories;
using CareerOS.Server.Repositories.EfCore;
using CareerOS.Server.Services;

namespace CareerOS.Server.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the portfolio feature's repository and service layers.
    /// Repositories are bound to their EF Core / PostgreSQL implementations.
    /// Both repositories and services are Scoped — they (transitively) hold
    /// a <see cref="Data.CareerOSDbContext"/>, which is itself Scoped, so
    /// nothing above this layer can be a Singleton.
    ///
    /// The <c>Repositories.InMemory</c> implementations are kept in the
    /// codebase (unused here) for fast unit tests that don't need a real
    /// database — swapping them back in only requires changing the
    /// registrations below.
    /// </summary>
    public static IServiceCollection AddPortfolioFeature(this IServiceCollection services)
    {
        services.AddScoped<IProfileRepository, EfCoreProfileRepository>();
        services.AddScoped<IExperienceRepository, EfCoreExperienceRepository>();
        services.AddScoped<IProjectRepository, EfCoreProjectRepository>();
        services.AddScoped<ISkillRepository, EfCoreSkillRepository>();
        services.AddScoped<ICredentialsRepository, EfCoreCredentialsRepository>();

        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ICredentialsService, CredentialsService>();

        return services;
    }
}
