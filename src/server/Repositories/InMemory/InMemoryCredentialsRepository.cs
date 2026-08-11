using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories.InMemory;

/// <summary>
/// Hardcoded seed data standing in for a real data store — see
/// <see cref="InMemoryProfileRepository"/> for the swap-out rationale.
/// </summary>
public class InMemoryCredentialsRepository : ICredentialsRepository
{
    private static readonly List<Certification> SeedCertifications =
    [
        new Certification { Id = Guid.NewGuid(), Name = "Azure Associate Developer (AZ-204)", Date = "March 2021" },
        new Certification { Id = Guid.NewGuid(), Name = "Power Platform Fundamentals (PL-900)", Date = "January 2022" },
        new Certification { Id = Guid.NewGuid(), Name = "RPA Developer Foundation Training", Date = "August 2019" },
    ];

    private static EducationEntry SeedEducation = new()
    {
        School = "Government College University",
        Degree = "Bachelor of Computer Science",
        Location = "Lahore, Pakistan",
        Date = "June 2017",
    };

    public Task<IReadOnlyList<Certification>> GetCertificationsAsync() =>
        Task.FromResult<IReadOnlyList<Certification>>(SeedCertifications);

    public Task<Certification> CreateCertificationAsync(CertificationRequest request)
    {
        var certification = new Certification { Id = Guid.NewGuid(), Name = request.Name, Date = request.Date };
        SeedCertifications.Add(certification);
        return Task.FromResult(certification);
    }

    public Task UpdateCertificationAsync(Guid id, CertificationRequest request)
    {
        var index = SeedCertifications.FindIndex(c => c.Id == id);
        if (index < 0)
        {
            throw new KeyNotFoundException($"Certification '{id}' not found.");
        }

        SeedCertifications[index] = new Certification { Id = id, Name = request.Name, Date = request.Date };
        return Task.CompletedTask;
    }

    public Task DeleteCertificationAsync(Guid id)
    {
        var removed = SeedCertifications.RemoveAll(c => c.Id == id);
        if (removed == 0)
        {
            throw new KeyNotFoundException($"Certification '{id}' not found.");
        }
        return Task.CompletedTask;
    }

    public Task<EducationEntry> GetEducationAsync() => Task.FromResult(SeedEducation);

    public Task UpdateEducationAsync(EducationEntryRequest request)
    {
        SeedEducation = new EducationEntry
        {
            School = request.School,
            Degree = request.Degree,
            Location = request.Location,
            Date = request.Date,
        };
        return Task.CompletedTask;
    }
}
