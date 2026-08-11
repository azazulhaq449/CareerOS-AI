using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories.InMemory;

/// <summary>
/// Hardcoded seed data standing in for a real data store. No database has
/// been chosen yet — swapping this out later only requires a new
/// <see cref="IProfileRepository"/> implementation and a DI registration
/// change in Program.cs; nothing above this layer needs to change.
/// </summary>
public class InMemoryProfileRepository : IProfileRepository
{
    private static Profile SeedProfile = new()
    {
        Id = Guid.NewGuid(),
        Name = "Azaz ul Haq",
        Initials = "AH",
        Title = "Senior Software Engineer",
        Roles = ["Senior .NET Engineer", "Cloud Solutions Architect", "AI-Integrated Developer"],
        Location = "Manchester, United Kingdom",
        Email = "azaz.ulhaqq@gmail.com",
        Phone = "+44 7774 031733",
        PhoneHref = "tel:+447774031733",
        LinkedInUrl = "https://www.linkedin.com/in/azaz-ul-haq-dev/",
        Summary =
        [
            "Software Developer with 8+ years of experience delivering .NET-based web and enterprise applications within Agile environments. Microsoft Azure-certified developer with hands-on experience across the full software development lifecycle, from requirements analysis and system design to implementation and deployment.",
            "Strong expertise in .NET Core (.NET 6-8), C#, MVC, RESTful Web APIs, WCF, SQL Server, and CMS platforms including Umbraco and Optimizely. Proven experience building and deploying cloud-native solutions on Microsoft Azure, including Azure App Services, Azure Functions, Cosmos DB, and secure API integrations.",
            "Experienced in implementing AI-powered features by integrating OpenAI APIs for automation and enhanced user experiences. Recognised as a Top-Rated Upwork developer, known for collaborative teamwork, mentoring junior developers, and delivering scalable, high-quality solutions.",
        ],
    };

    private static readonly List<Strength> SeedStrengths =
    [
        new Strength
        {
            Id = Guid.NewGuid(),
            Icon = "uil-server",
            Title = "End-to-End .NET Delivery",
            Description =
                "8+ years shipping .NET 6-8 web and enterprise applications, from requirements and system design through implementation, testing and deployment.",
        },
        new Strength
        {
            Id = Guid.NewGuid(),
            Icon = "uil-cloud-computing",
            Title = "Cloud-Native on Azure",
            Description =
                "Azure-certified (AZ-204) architect of App Services, Functions, Container Apps and Cosmos DB solutions, applying Clean Architecture and DDD.",
        },
        new Strength
        {
            Id = Guid.NewGuid(),
            Icon = "uil-robot",
            Title = "AI-Powered Features",
            Description =
                "Integrates OpenAI APIs for automation, prompt engineering and self-learning workflows that enhance real-world user experiences.",
        },
    ];

    public Task<Profile> GetProfileAsync() => Task.FromResult(SeedProfile);

    public Task UpdateProfileAsync(ProfileRequest request)
    {
        SeedProfile = new Profile
        {
            Id = SeedProfile.Id,
            Name = request.Name,
            Initials = request.Initials,
            Title = request.Title,
            Roles = request.Roles,
            Location = request.Location,
            Email = request.Email,
            Phone = request.Phone,
            PhoneHref = request.PhoneHref,
            LinkedInUrl = request.LinkedInUrl,
            Summary = request.Summary,
        };
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Strength>> GetStrengthsAsync() =>
        Task.FromResult<IReadOnlyList<Strength>>(SeedStrengths);

    public Task<Strength> CreateStrengthAsync(StrengthRequest request)
    {
        var strength = new Strength { Id = Guid.NewGuid(), Icon = request.Icon, Title = request.Title, Description = request.Description };
        SeedStrengths.Add(strength);
        return Task.FromResult(strength);
    }

    public Task UpdateStrengthAsync(Guid id, StrengthRequest request)
    {
        var index = SeedStrengths.FindIndex(s => s.Id == id);
        if (index < 0)
        {
            throw new KeyNotFoundException($"Strength '{id}' not found.");
        }

        SeedStrengths[index] = new Strength { Id = id, Icon = request.Icon, Title = request.Title, Description = request.Description };
        return Task.CompletedTask;
    }

    public Task DeleteStrengthAsync(Guid id)
    {
        var removed = SeedStrengths.RemoveAll(s => s.Id == id);
        if (removed == 0)
        {
            throw new KeyNotFoundException($"Strength '{id}' not found.");
        }
        return Task.CompletedTask;
    }
}
