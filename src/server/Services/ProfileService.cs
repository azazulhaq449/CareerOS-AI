using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

public class ProfileService(IProfileRepository repository) : IProfileService
{
    public Task<Profile> GetProfileAsync() => repository.GetProfileAsync();

    public Task UpdateProfileAsync(ProfileRequest request) => repository.UpdateProfileAsync(request);

    public Task<IReadOnlyList<Strength>> GetStrengthsAsync() => repository.GetStrengthsAsync();

    public Task<Strength> CreateStrengthAsync(StrengthRequest request) => repository.CreateStrengthAsync(request);

    public Task UpdateStrengthAsync(Guid id, StrengthRequest request) => repository.UpdateStrengthAsync(id, request);

    public Task DeleteStrengthAsync(Guid id) => repository.DeleteStrengthAsync(id);
}
