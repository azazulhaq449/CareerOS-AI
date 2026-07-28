using CareerOS.Server.Models;
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
}
