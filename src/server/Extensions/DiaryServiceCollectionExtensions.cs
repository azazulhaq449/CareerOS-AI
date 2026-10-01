using CareerOS.Server.Repositories;
using CareerOS.Server.Repositories.EfCore;
using CareerOS.Server.Services;

namespace CareerOS.Server.Extensions;

public static class DiaryServiceCollectionExtensions
{
    /// <summary>
    /// Registers the engineering diary feature's repository and service
    /// layers. Kept separate from <see cref="ServiceCollectionExtensions.AddPortfolioFeature"/>
    /// since the diary is private admin content, not public portfolio data.
    /// </summary>
    public static IServiceCollection AddDiaryFeature(this IServiceCollection services)
    {
        services.AddScoped<IDiaryRepository, EfCoreDiaryRepository>();
        services.AddScoped<IDiaryService, DiaryService>();

        return services;
    }
}
