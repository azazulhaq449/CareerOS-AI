using CareerOS.Server.Models;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class ExperienceService(IExperienceRepository repository) : IExperienceService
{
    public Task<IReadOnlyList<ExperienceEntry>> GetAllAsync() => repository.GetAllAsync();
}
