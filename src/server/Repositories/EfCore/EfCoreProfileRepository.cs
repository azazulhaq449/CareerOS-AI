using CareerOS.Server.Data;
using CareerOS.Server.Data.Entities;
using CareerOS.Server.Data.Mapping;
using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Repositories.EfCore;

public class EfCoreProfileRepository(CareerOSDbContext context) : IProfileRepository
{
    public async Task<Profile> GetProfileAsync()
    {
        var entity = await context.Profiles.SingleAsync();
        return entity.ToModel();
    }

    public async Task UpdateProfileAsync(ProfileRequest request)
    {
        var entity = await context.Profiles.SingleAsync();
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Strength>> GetStrengthsAsync()
    {
        var entities = await context.Strengths.OrderBy(s => s.SortOrder).ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<Strength> CreateStrengthAsync(StrengthRequest request)
    {
        var entity = request.ToEntity();
        context.Strengths.Add(entity);
        await context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task UpdateStrengthAsync(Guid id, StrengthRequest request)
    {
        var entity = await GetStrengthOrThrowAsync(id);
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteStrengthAsync(Guid id)
    {
        var entity = await GetStrengthOrThrowAsync(id);
        context.Strengths.Remove(entity);
        await context.SaveChangesAsync();
    }

    private async Task<StrengthEntity> GetStrengthOrThrowAsync(Guid id) =>
        await context.Strengths.FindAsync(id)
            ?? throw new KeyNotFoundException($"Strength '{id}' not found.");
}
