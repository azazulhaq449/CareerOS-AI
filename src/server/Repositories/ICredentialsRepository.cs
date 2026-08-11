using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories;

public interface ICredentialsRepository
{
    Task<IReadOnlyList<Certification>> GetCertificationsAsync();
    Task<Certification> CreateCertificationAsync(CertificationRequest request);
    Task UpdateCertificationAsync(Guid id, CertificationRequest request);
    Task DeleteCertificationAsync(Guid id);

    Task<EducationEntry> GetEducationAsync();
    Task UpdateEducationAsync(EducationEntryRequest request);
}
