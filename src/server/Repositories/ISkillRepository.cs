using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories;

public interface ISkillRepository
{
    Task<IReadOnlyList<SkillGroup>> GetAllAsync();
    Task<SkillGroup?> GetByIdAsync(Guid id);
    Task<SkillGroup> CreateAsync(SkillGroupRequest request);
    Task UpdateAsync(Guid id, SkillGroupRequest request);
    Task DeleteAsync(Guid id);
}
