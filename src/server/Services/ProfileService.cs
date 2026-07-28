using CareerOS.Server.Models;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

/// <summary>
/// Application-layer service for profile data. Controllers depend on this
/// abstraction, never on <see cref="IProfileRepository"/> directly — keeping
/// data access and business/orchestration logic in separate, independently
/// replaceable layers (dependency inversion).
/// </summary>
public class ProfileService(IProfileRepository repository) : IProfileService
{
    public Task<Profile> GetProfileAsync() => repository.GetProfileAsync();

    public Task<IReadOnlyList<Strength>> GetStrengthsAsync() => repository.GetStrengthsAsync();
}
