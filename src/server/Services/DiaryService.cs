using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class DiaryService(IDiaryRepository repository) : IDiaryService
{
    public Task<IReadOnlyList<DiaryEntry>> SearchAsync(string? search, string? category, string? tag) =>
        repository.SearchAsync(search, category, tag);

    public Task<DiaryEntry?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

    public Task<DiaryEntry> CreateAsync(DiaryEntryRequest request) => repository.CreateAsync(request);

    public Task UpdateAsync(Guid id, DiaryEntryRequest request) => repository.UpdateAsync(id, request);

    public Task DeleteAsync(Guid id) => repository.DeleteAsync(id);
}
