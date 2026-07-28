namespace CareerOS.Server.Models;

/// <summary>
/// Composite response combining certifications and education — assembled by
/// <see cref="Services.ICredentialsService"/> from two separate repository reads.
/// </summary>
public class CredentialsResponse
{
    public required IReadOnlyList<Certification> Certifications { get; init; }
    public required EducationEntry Education { get; init; }
}
