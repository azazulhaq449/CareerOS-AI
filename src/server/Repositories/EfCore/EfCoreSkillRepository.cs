using CareerOS.Server.Data;
using CareerOS.Server.Data.Mapping;
using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Repositories.EfCore;

public class EfCoreSkillRepository(CareerOSDbContext context) : ISkillRepository
{
    public async Task<IReadOnlyList<SkillGroup>> GetAllAsync()
    {
        var entities = await context.SkillGroups.OrderBy(s => s.SortOrder).ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<SkillGroup?> GetByIdAsync(Guid id)
    {
        var entity = await context.SkillGroups.FindAsync(id);
        return entity?.ToModel();
    }

    public async Task<SkillGroup> CreateAsync(SkillGroupRequest request)
    {
        var entity = request.ToEntity();
        context.SkillGroups.Add(entity);
        await context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task UpdateAsync(Guid id, SkillGroupRequest request)
    {
        var entity = await context.SkillGroups.FindAsync(id)
            ?? throw new KeyNotFoundException($"Skill group '{id}' not found.");
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.SkillGroups.FindAsync(id)
            ?? throw new KeyNotFoundException($"Skill group '{id}' not found.");
        context.SkillGroups.Remove(entity);
        await context.SaveChangesAsync();
    }
}
