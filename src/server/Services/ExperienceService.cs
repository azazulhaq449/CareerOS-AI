using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class ExperienceService(IExperienceRepository repository) : IExperienceService
{
    public Task<IReadOnlyList<ExperienceEntry>> GetAllAsync() => repository.GetAllAsync();

    public Task<ExperienceEntry?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

    public Task<ExperienceEntry> CreateAsync(ExperienceEntryRequest request) => repository.CreateAsync(request);

    public Task UpdateAsync(Guid id, ExperienceEntryRequest request) => repository.UpdateAsync(id, request);

    public Task DeleteAsync(Guid id) => repository.DeleteAsync(id);
}
