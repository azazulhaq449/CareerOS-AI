using CareerOS.Server.Models;

namespace CareerOS.Server.Services;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectEntry>> GetAllAsync();
}
