using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories;

public interface IProjectRepository
{
    Task<IReadOnlyList<ProjectEntry>> GetAllAsync();
    Task<ProjectEntry?> GetByIdAsync(Guid id);
    Task<ProjectEntry> CreateAsync(ProjectEntryRequest request);
    Task UpdateAsync(Guid id, ProjectEntryRequest request);
    Task DeleteAsync(Guid id);
}
