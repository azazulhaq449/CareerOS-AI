using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Services;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectEntry>> GetAllAsync();
    Task<ProjectEntry?> GetByIdAsync(Guid id);
    Task<ProjectEntry> CreateAsync(ProjectEntryRequest request);
    Task UpdateAsync(Guid id, ProjectEntryRequest request);
    Task DeleteAsync(Guid id);
}
