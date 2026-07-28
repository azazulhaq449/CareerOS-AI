using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories.InMemory;

/// <summary>
/// Hardcoded seed data standing in for a real data store — see
/// <see cref="InMemoryProfileRepository"/> for the swap-out rationale.
/// </summary>
public class InMemoryProjectRepository : IProjectRepository
{
    private static readonly IReadOnlyList<ProjectEntry> SeedProjects =
    [
        new ProjectEntry
        {
            Name = "KidsPass",
            Organisation = "Digital Reward Group",
            Icon = "uil-ticket",
            Description =
                "Multi-brand membership platform sharing one codebase across KidsPass, PopcornPass, FamilyPass and GlobalHotelPass, with Adyen/Google Wallet payments and Entra ID auth.",
            Tags = [".NET 8", "Azure", "Adyen", "Entra ID", "Quartz.NET"],
        },
        new ProjectEntry
        {
            Name = "Meter Companion",
            Organisation = "Autocab",
            Icon = "uil-taxi",
            Description =
                "Greenfield Blazor application for taxi dispatch and booking, with RESTful backend services and a Corporate Portal built on MudBlazor and QuickGrid.",
            Tags = ["Blazor", ".NET 7", "MudBlazor", "Azure AD B2C"],
        },
        new ProjectEntry
        {
            Name = "Instamail AI",
            Organisation = "Upwork",
            Icon = "uil-robot",
            Description =
                "Task scheduling and automation platform integrating Google API and Microsoft Graph for email/calendar management, with OpenAI-driven automation workflows.",
            Tags = [".NET 8", "Quartz.NET", "OpenAI API", "MongoDB"],
        },
        new ProjectEntry
        {
            Name = "Pattern.com",
            Organisation = "Upwork",
            Icon = "uil-sitemap",
            Description =
                "Cloud-integrated automation platform combining Azure Functions, Pipedrive and Snowflake APIs with UiPath for repetitive task automation, plus a React front end.",
            Tags = ["Azure Functions", "UiPath", "React", ".NET APIs"],
        },
        new ProjectEntry
        {
            Name = "WORKS Performance Monitor",
            Organisation = "Strategic Systems International",
            Icon = "uil-chart-line",
            Description =
                "Real-time monitoring system for ASMPT SMT production lines (Germany) built on a microservice architecture with RabbitMQ messaging.",
            Tags = ["Angular 13", ".NET", "WCF", "RabbitMQ", "TPL"],
        },
        new ProjectEntry
        {
            Name = "WORKS Material Tower Cluster Controller",
            Organisation = "Strategic Systems International",
            Icon = "uil-database",
            Description =
                "Software for managing material storage and retrieval across Material Towers, built with strong OOP and data structure design.",
            Tags = [".NET MVC", "C#", "WCF", "TPL"],
        },
        new ProjectEntry
        {
            Name = "Deal.Bargains",
            Organisation = "Ai logica",
            Icon = "uil-shopping-cart",
            Description =
                "E-commerce affiliate platform for category, brand and retailer management, with SignalR real-time updates and Apache Solr search.",
            Tags = [".NET MVC", "Umbraco", "SQL Server", "MongoDB", "SignalR"],
        },
        new ProjectEntry
        {
            Name = "Millimeter",
            Organisation = "Ai logica",
            Icon = "uil-link-alt",
            Description =
                "Secure blockchain system for land data storage using peer-to-peer programming and high-level encryption.",
            Tags = [".NET", "WCF", "Blockchain"],
        },
        new ProjectEntry
        {
            Name = "National History Museum",
            Organisation = "Ai logica",
            Icon = "uil-university",
            Description =
                "Museum website covering exhibits, press releases, events and volunteer management, with integrated e-commerce and payments.",
            Tags = [".NET MVC", "SQL Server"],
        },
    ];

    public Task<IReadOnlyList<ProjectEntry>> GetAllAsync() => Task.FromResult(SeedProjects);
}
