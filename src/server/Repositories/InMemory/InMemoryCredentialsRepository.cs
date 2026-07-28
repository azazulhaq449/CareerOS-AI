using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories.InMemory;

/// <summary>
/// Hardcoded seed data standing in for a real data store — see
/// <see cref="InMemoryProfileRepository"/> for the swap-out rationale.
/// </summary>
public class InMemoryCredentialsRepository : ICredentialsRepository
{
    private static readonly IReadOnlyList<Certification> SeedCertifications =
    [
        new Certification { Name = "Azure Associate Developer (AZ-204)", Date = "March 2021" },
        new Certification { Name = "Power Platform Fundamentals (PL-900)", Date = "January 2022" },
        new Certification { Name = "RPA Developer Foundation Training", Date = "August 2019" },
    ];

    private static readonly EducationEntry SeedEducation = new()
    {
        School = "Government College University",
        Degree = "Bachelor of Computer Science",
        Location = "Lahore, Pakistan",
        Date = "June 2017",
    };

    public Task<IReadOnlyList<Certification>> GetCertificationsAsync() => Task.FromResult(SeedCertifications);

    public Task<EducationEntry> GetEducationAsync() => Task.FromResult(SeedEducation);
}
