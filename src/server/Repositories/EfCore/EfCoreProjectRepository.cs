using CareerOS.Server.Data;
using CareerOS.Server.Data.Mapping;
using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Repositories.EfCore;

public class EfCoreProjectRepository(CareerOSDbContext context) : IProjectRepository
{
    public async Task<IReadOnlyList<ProjectEntry>> GetAllAsync()
    {
        var entities = await context.ProjectEntries.OrderBy(p => p.SortOrder).ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<ProjectEntry?> GetByIdAsync(Guid id)
    {
        var entity = await context.ProjectEntries.FindAsync(id);
        return entity?.ToModel();
    }

    public async Task<ProjectEntry> CreateAsync(ProjectEntryRequest request)
    {
        var entity = request.ToEntity();
        context.ProjectEntries.Add(entity);
        await context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task UpdateAsync(Guid id, ProjectEntryRequest request)
    {
        var entity = await context.ProjectEntries.FindAsync(id)
            ?? throw new KeyNotFoundException($"Project entry '{id}' not found.");
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.ProjectEntries.FindAsync(id)
            ?? throw new KeyNotFoundException($"Project entry '{id}' not found.");
        context.ProjectEntries.Remove(entity);
        await context.SaveChangesAsync();
    }
}
