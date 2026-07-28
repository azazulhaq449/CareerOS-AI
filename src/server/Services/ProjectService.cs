using CareerOS.Server.Models;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class ProjectService(IProjectRepository repository) : IProjectService
{
    public Task<IReadOnlyList<ProjectEntry>> GetAllAsync() => repository.GetAllAsync();
}
