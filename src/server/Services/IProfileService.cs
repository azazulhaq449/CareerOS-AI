using CareerOS.Server.Models;

namespace CareerOS.Server.Services;

public interface IProfileService
{
    Task<Profile> GetProfileAsync();
    Task<IReadOnlyList<Strength>> GetStrengthsAsync();
}
