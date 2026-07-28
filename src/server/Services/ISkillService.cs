using CareerOS.Server.Models;

namespace CareerOS.Server.Services;

public interface ISkillService
{
    Task<IReadOnlyList<SkillGroup>> GetAllAsync();
}
