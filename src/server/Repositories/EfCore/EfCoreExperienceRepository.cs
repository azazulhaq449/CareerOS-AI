using CareerOS.Server.Data;
using CareerOS.Server.Data.Mapping;
using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Repositories.EfCore;

public class EfCoreExperienceRepository(CareerOSDbContext context) : IExperienceRepository
{
    public async Task<IReadOnlyList<ExperienceEntry>> GetAllAsync()
    {
        var entities = await context.ExperienceEntries.OrderBy(e => e.SortOrder).ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<ExperienceEntry?> GetByIdAsync(Guid id)
    {
        var entity = await context.ExperienceEntries.FindAsync(id);
        return entity?.ToModel();
    }

    public async Task<ExperienceEntry> CreateAsync(ExperienceEntryRequest request)
    {
        var entity = request.ToEntity();
        context.ExperienceEntries.Add(entity);
        await context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task UpdateAsync(Guid id, ExperienceEntryRequest request)
    {
        var entity = await context.ExperienceEntries.FindAsync(id)
            ?? throw new KeyNotFoundException($"Experience entry '{id}' not found.");
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.ExperienceEntries.FindAsync(id)
            ?? throw new KeyNotFoundException($"Experience entry '{id}' not found.");
        context.ExperienceEntries.Remove(entity);
        await context.SaveChangesAsync();
    }
}
