using CareerOS.Server.Repositories;
using CareerOS.Server.Repositories.InMemory;
using CareerOS.Server.Services;

namespace CareerOS.Server.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the portfolio feature's repository and service layers.
    /// Repositories are bound to their in-memory implementations for now —
    /// no database has been chosen yet. When one is, only the repository
    /// registrations below need to change (e.g. to Scoped, EF-Core-backed
    /// implementations); services and controllers depend solely on the
    /// repository interfaces and are unaffected.
    /// </summary>
    public static IServiceCollection AddPortfolioFeature(this IServiceCollection services)
    {
        services.AddSingleton<IProfileRepository, InMemoryProfileRepository>();
        services.AddSingleton<IExperienceRepository, InMemoryExperienceRepository>();
        services.AddSingleton<IProjectRepository, InMemoryProjectRepository>();
        services.AddSingleton<ISkillRepository, InMemorySkillRepository>();
        services.AddSingleton<ICredentialsRepository, InMemoryCredentialsRepository>();

        services.AddSingleton<IProfileService, ProfileService>();
        services.AddSingleton<IExperienceService, ExperienceService>();
        services.AddSingleton<IProjectService, ProjectService>();
        services.AddSingleton<ISkillService, SkillService>();
        services.AddSingleton<ICredentialsService, CredentialsService>();

        return services;
    }
}
