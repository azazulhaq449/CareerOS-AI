using CareerOS.Server.Models;

namespace CareerOS.Server.Repositories.InMemory;

/// <summary>
/// Hardcoded seed data standing in for a real data store — see
/// <see cref="InMemoryProfileRepository"/> for the swap-out rationale.
/// </summary>
public class InMemorySkillRepository : ISkillRepository
{
    private static readonly IReadOnlyList<SkillGroup> SeedSkillGroups =
    [
        new SkillGroup
        {
            Category = "Backend & APIs",
            Icon = "uil-server",
            Items = [".NET Core", ".NET MVC", "C#", "Web API", "WCF", "SignalR"],
        },
        new SkillGroup
        {
            Category = "Cloud & DevOps",
            Icon = "uil-cloud-computing",
            Items = ["Microsoft Azure", "AWS", "Azure Functions", "Azure App Services", "Git", "TFS"],
        },
        new SkillGroup
        {
            Category = "Frontend",
            Icon = "uil-window-section",
            Items = ["React", "Angular", "Blazor", "HTML/CSS/Bootstrap", "JavaScript", "WPF"],
        },
        new SkillGroup
        {
            Category = "Data & Messaging",
            Icon = "uil-database",
            Items = ["SQL Server", "MongoDB", "RabbitMQ"],
        },
        new SkillGroup
        {
            Category = "Platforms & Automation",
            Icon = "uil-cog",
            Items = ["Umbraco", "Optimizely", "UiPath"],
        },
    ];

    public Task<IReadOnlyList<SkillGroup>> GetAllAsync() => Task.FromResult(SeedSkillGroups);
}
