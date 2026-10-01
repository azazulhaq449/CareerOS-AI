using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories;

public interface IDiaryRepository
{
    Task<IReadOnlyList<DiaryEntry>> SearchAsync(string? search, string? category, string? tag);
    Task<DiaryEntry?> GetByIdAsync(Guid id);
    Task<DiaryEntry> CreateAsync(DiaryEntryRequest request);
    Task UpdateAsync(Guid id, DiaryEntryRequest request);
    Task DeleteAsync(Guid id);
}
