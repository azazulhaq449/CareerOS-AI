using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Repositories;

namespace CareerOS.Server.Services;

/// <summary>
/// Composes certifications and education — two independent repository reads
/// — into the single response the Credentials section needs. This kind of
/// aggregation belongs in the service layer, not the repository layer.
/// </summary>
public class CredentialsService(ICredentialsRepository repository) : ICredentialsService
{
    public async Task<CredentialsResponse> GetCredentialsAsync()
    {
        var certifications = await repository.GetCertificationsAsync();
        var education = await repository.GetEducationAsync();

        return new CredentialsResponse
        {
            Certifications = certifications,
            Education = education,
        };
    }

    public Task<Certification> CreateCertificationAsync(CertificationRequest request) =>
        repository.CreateCertificationAsync(request);

    public Task UpdateCertificationAsync(Guid id, CertificationRequest request) =>
        repository.UpdateCertificationAsync(id, request);

    public Task DeleteCertificationAsync(Guid id) => repository.DeleteCertificationAsync(id);

    public Task UpdateEducationAsync(EducationEntryRequest request) => repository.UpdateEducationAsync(request);
}
