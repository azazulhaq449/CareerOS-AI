using CareerOS.Server.Models;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class SkillService(ISkillRepository repository) : ISkillService
{
    public Task<IReadOnlyList<SkillGroup>> GetAllAsync() => repository.GetAllAsync();
}
