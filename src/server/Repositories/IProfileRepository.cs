using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories;

public interface IProfileRepository
{
    Task<Profile> GetProfileAsync();
    Task<IReadOnlyList<Strength>> GetStrengthsAsync();
}
