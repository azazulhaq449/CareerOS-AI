using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories;

public interface ISkillRepository
{
    Task<IReadOnlyList<SkillGroup>> GetAllAsync();
}
