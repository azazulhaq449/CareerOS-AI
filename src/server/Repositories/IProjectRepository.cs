using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories;

public interface IProjectRepository
{
    Task<IReadOnlyList<ProjectEntry>> GetAllAsync();
}
