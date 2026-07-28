using CareerOS.Server.Models;

namespace CareerOS.Server.Services;

public interface IExperienceService
{
    Task<IReadOnlyList<ExperienceEntry>> GetAllAsync();
}
