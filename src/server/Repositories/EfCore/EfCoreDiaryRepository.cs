using CareerOS.Server.Data;
using CareerOS.Server.Data.Mapping;
using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Repositories.EfCore;

public class EfCoreDiaryRepository(CareerOSDbContext context) : IDiaryRepository
{
    public async Task<IReadOnlyList<DiaryEntry>> SearchAsync(string? search, string? category, string? tag)
    {
        var query = context.DiaryEntries.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(e => e.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(tag))
        {
            query = query.Where(e => e.Tags.Contains(tag));
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e =>
                EF.Functions.ILike(e.Title, $"%{search}%") ||
                EF.Functions.ILike(e.Content, $"%{search}%"));
        }

        var entities = await query.OrderByDescending(e => e.EntryDate).ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<DiaryEntry?> GetByIdAsync(Guid id)
    {
        var entity = await context.DiaryEntries.FindAsync(id);
        return entity?.ToModel();
    }

    public async Task<DiaryEntry> CreateAsync(DiaryEntryRequest request)
    {
        var entity = request.ToEntity();
        context.DiaryEntries.Add(entity);
        await context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task UpdateAsync(Guid id, DiaryEntryRequest request)
    {
        var entity = await context.DiaryEntries.FindAsync(id)
            ?? throw new KeyNotFoundException($"Diary entry '{id}' not found.");
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.DiaryEntries.FindAsync(id)
            ?? throw new KeyNotFoundException($"Diary entry '{id}' not found.");
        context.DiaryEntries.Remove(entity);
        await context.SaveChangesAsync();
    }
}
