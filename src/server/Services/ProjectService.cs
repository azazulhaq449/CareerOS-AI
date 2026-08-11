using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class ProjectService(IProjectRepository repository) : IProjectService
{
    public Task<IReadOnlyList<ProjectEntry>> GetAllAsync() => repository.GetAllAsync();

    public Task<ProjectEntry?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

    public Task<ProjectEntry> CreateAsync(ProjectEntryRequest request) => repository.CreateAsync(request);

    public Task UpdateAsync(Guid id, ProjectEntryRequest request) => repository.UpdateAsync(id, request);

    public Task DeleteAsync(Guid id) => repository.DeleteAsync(id);
}
