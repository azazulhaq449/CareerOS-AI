using CareerOS.Server.Data;
using CareerOS.Server.Data.Mapping;
using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Repositories.EfCore;

public class EfCoreCredentialsRepository(CareerOSDbContext context) : ICredentialsRepository
{
    public async Task<IReadOnlyList<Certification>> GetCertificationsAsync()
    {
        var entities = await context.Certifications.OrderBy(c => c.SortOrder).ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<Certification> CreateCertificationAsync(CertificationRequest request)
    {
        var entity = request.ToEntity();
        context.Certifications.Add(entity);
        await context.SaveChangesAsync();
        return entity.ToModel();
    }

    public async Task UpdateCertificationAsync(Guid id, CertificationRequest request)
    {
        var entity = await context.Certifications.FindAsync(id)
            ?? throw new KeyNotFoundException($"Certification '{id}' not found.");
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCertificationAsync(Guid id)
    {
        var entity = await context.Certifications.FindAsync(id)
            ?? throw new KeyNotFoundException($"Certification '{id}' not found.");
        context.Certifications.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<EducationEntry> GetEducationAsync()
    {
        var entity = await context.EducationEntries.SingleAsync();
        return entity.ToModel();
    }

    public async Task UpdateEducationAsync(EducationEntryRequest request)
    {
        var entity = await context.EducationEntries.SingleAsync();
        request.ApplyTo(entity);
        await context.SaveChangesAsync();
    }
}
