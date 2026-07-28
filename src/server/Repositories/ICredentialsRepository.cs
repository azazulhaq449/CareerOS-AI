using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories;

public interface ICredentialsRepository
{
    Task<IReadOnlyList<Certification>> GetCertificationsAsync();
    Task<EducationEntry> GetEducationAsync();
}
