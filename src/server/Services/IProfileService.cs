using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Services;

public interface IProfileService
{
    Task<Profile> GetProfileAsync();
    Task UpdateProfileAsync(ProfileRequest request);

    Task<IReadOnlyList<Strength>> GetStrengthsAsync();
    Task<Strength> CreateStrengthAsync(StrengthRequest request);
    Task UpdateStrengthAsync(Guid id, StrengthRequest request);
    Task DeleteStrengthAsync(Guid id);
}
