using CareerOS.Server.Models;

namespace CareerOS.Server.Services;

public interface ICredentialsService
{
    Task<CredentialsResponse> GetCredentialsAsync();
}
