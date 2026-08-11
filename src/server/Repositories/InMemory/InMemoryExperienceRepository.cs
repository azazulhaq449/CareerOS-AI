using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories.InMemory;

/// <summary>
/// Hardcoded seed data standing in for a real data store — see
/// <see cref="InMemoryProfileRepository"/> for the swap-out rationale.
/// </summary>
public class InMemoryExperienceRepository : IExperienceRepository
{
    private static readonly List<ExperienceEntry> SeedExperience =
    [
        new ExperienceEntry
        {
            Id = Guid.NewGuid(),
            Company = "Digital Reward Group",
            Location = "Manchester, UK",
            Role = "Senior Software Developer",
            Period = "March 2024 – Present",
            Projects = "KidsPass, DRG Admin, B2B Portal, Public & Partner APIs",
            Highlights =
            [
                "Design, develop and maintain .NET 8 RESTful APIs, Web APIs and MVC applications supporting high-traffic, multi-brand platforms including KidsPass, PopcornPass, FamilyPass and GlobalHotelPass.",
                "Architect scalable n-tier and microservices-based solutions, applying SOLID principles, Clean Architecture and domain-driven design (DDD).",
                "Implement secure, compliant payment solutions using Adyen (Drop-in, Express Checkout, Apple Pay, Google Pay, 3DS, recurring billing) and Google Wallet.",
                "Manage authentication and authorisation using Microsoft Entra ID with external identity providers and role-based access control (RBAC).",
                "Deploy and operate services on Azure App Services, Functions and Container Apps, using Azure Key Vault for secrets management.",
            ],
        },
        new ExperienceEntry
        {
            Id = Guid.NewGuid(),
            Company = "Autocab",
            Location = "Manchester, UK",
            Role = "Software Developer",
            Period = "Aug 2023 – March 2024",
            Projects = "Meter Companion, Booking & Dispatch Ghost API",
            Highlights =
            [
                "Developed and maintained .NET 7 Web APIs with a strong focus on RESTful API design and backend performance.",
                "Designed and implemented n-tier architecture following SOLID principles, improving scalability and maintainability across services.",
                "Built a Blazor application from scratch, delivering interactive, data-driven UIs with a modern component-based architecture.",
                "Integrated Azure AD B2C authentication and authorisation to secure user access and identity management.",
            ],
        },
        new ExperienceEntry
        {
            Id = Guid.NewGuid(),
            Company = "Strategic Systems International",
            Location = "UK",
            Role = "Senior Software Engineer",
            Period = "March 2019 – Aug 2023",
            Projects = "WORKS Performance Monitor, WORKS Material Tower Cluster Controller",
            Highlights =
            [
                "Implemented SMT industrial messaging architecture (CFX) with RabbitMQ for real-time production line monitoring.",
                "Developed backend service layers using .NET, C#, Task Parallel Library and WCF Services.",
                "Participated in migrating the web frontend from AngularJS to Angular 13.",
                "Practised Unit Test Driven Development (UTDD), running workshops and documentation for the wider team.",
                "Mentored team members and provided solutions for complex technical issues across multiple projects.",
            ],
        },
        new ExperienceEntry
        {
            Id = Guid.NewGuid(),
            Company = "Ai logica",
            Location = "Remote, UK",
            Role = "Full Stack .NET Developer",
            Period = "July 2017 – March 2019",
            Projects = "Deal.Bargains, Millimeter",
            Highlights =
            [
                "Built an e-commerce web application in .NET using Umbraco CMS and Web API, handling over 10,000 brands with SQL Server and MongoDB.",
                "Enhanced search capability by integrating Apache Solr and built a module for uploading images to Amazon S3.",
                "Gained exposure to blockchain technology, contributing to a blockchain core project for crypto wallet development.",
                "Troubleshot and resolved bugs across .NET applications, working closely with senior developers on multiple products.",
            ],
        },
    ];

    public Task<IReadOnlyList<ExperienceEntry>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<ExperienceEntry>>(SeedExperience);

    public Task<ExperienceEntry?> GetByIdAsync(Guid id) =>
        Task.FromResult(SeedExperience.FirstOrDefault(e => e.Id == id));

    public Task<ExperienceEntry> CreateAsync(ExperienceEntryRequest request)
    {
        var entry = new ExperienceEntry
        {
            Id = Guid.NewGuid(),
            Company = request.Company,
            Location = request.Location,
            Role = request.Role,
            Period = request.Period,
            Projects = request.Projects,
            Highlights = request.Highlights,
        };
        SeedExperience.Add(entry);
        return Task.FromResult(entry);
    }

    public Task UpdateAsync(Guid id, ExperienceEntryRequest request)
    {
        var index = SeedExperience.FindIndex(e => e.Id == id);
        if (index < 0)
        {
            throw new KeyNotFoundException($"Experience entry '{id}' not found.");
        }

        SeedExperience[index] = new ExperienceEntry
        {
            Id = id,
            Company = request.Company,
            Location = request.Location,
            Role = request.Role,
            Period = request.Period,
            Projects = request.Projects,
            Highlights = request.Highlights,
        };
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var removed = SeedExperience.RemoveAll(e => e.Id == id);
        if (removed == 0)
        {
            throw new KeyNotFoundException($"Experience entry '{id}' not found.");
        }
        return Task.CompletedTask;
    }
}
