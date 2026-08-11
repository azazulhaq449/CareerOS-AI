using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Services;

public interface ICredentialsService
{
    Task<CredentialsResponse> GetCredentialsAsync();

    Task<Certification> CreateCertificationAsync(CertificationRequest request);
    Task UpdateCertificationAsync(Guid id, CertificationRequest request);
    Task DeleteCertificationAsync(Guid id);

    Task UpdateEducationAsync(EducationEntryRequest request);
}
