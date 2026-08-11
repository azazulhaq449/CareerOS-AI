using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories;

public interface IExperienceRepository
{
    Task<IReadOnlyList<ExperienceEntry>> GetAllAsync();
    Task<ExperienceEntry?> GetByIdAsync(Guid id);
    Task<ExperienceEntry> CreateAsync(ExperienceEntryRequest request);
    Task UpdateAsync(Guid id, ExperienceEntryRequest request);
    Task DeleteAsync(Guid id);
}
