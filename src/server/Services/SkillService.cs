using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class SkillService(ISkillRepository repository) : ISkillService
{
    public Task<IReadOnlyList<SkillGroup>> GetAllAsync() => repository.GetAllAsync();

    public Task<SkillGroup?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

    public Task<SkillGroup> CreateAsync(SkillGroupRequest request) => repository.CreateAsync(request);

    public Task UpdateAsync(Guid id, SkillGroupRequest request) => repository.UpdateAsync(id, request);

    public Task DeleteAsync(Guid id) => repository.DeleteAsync(id);
}
