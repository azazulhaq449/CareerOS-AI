using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories;

public interface IExperienceRepository
{
    Task<IReadOnlyList<ExperienceEntry>> GetAllAsync();
}
