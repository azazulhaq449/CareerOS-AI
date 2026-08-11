using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;

namespace CareerOS.Server.Repositories.InMemory;

/// <summary>
/// Hardcoded seed data standing in for a real data store — see
/// <see cref="InMemoryProfileRepository"/> for the swap-out rationale.
/// </summary>
public class InMemorySkillRepository : ISkillRepository
{
    private static readonly List<SkillGroup> SeedSkillGroups =
    [
        new SkillGroup
        {
            Id = Guid.NewGuid(),
            Category = "Backend & APIs",
            Icon = "uil-server",
            Items = [".NET Core", ".NET MVC", "C#", "Web API", "WCF", "SignalR"],
        },
        new SkillGroup
        {
            Id = Guid.NewGuid(),
            Category = "Cloud & DevOps",
            Icon = "uil-cloud-computing",
            Items = ["Microsoft Azure", "AWS", "Azure Functions", "Azure App Services", "Git", "TFS"],
        },
        new SkillGroup
        {
            Id = Guid.NewGuid(),
            Category = "Frontend",
            Icon = "uil-window-section",
            Items = ["React", "Angular", "Blazor", "HTML/CSS/Bootstrap", "JavaScript", "WPF"],
        },
        new SkillGroup
        {
            Id = Guid.NewGuid(),
            Category = "Data & Messaging",
            Icon = "uil-database",
            Items = ["SQL Server", "MongoDB", "RabbitMQ"],
        },
        new SkillGroup
        {
            Id = Guid.NewGuid(),
            Category = "Platforms & Automation",
            Icon = "uil-cog",
            Items = ["Umbraco", "Optimizely", "UiPath"],
        },
    ];

    public Task<IReadOnlyList<SkillGroup>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<SkillGroup>>(SeedSkillGroups);

    public Task<SkillGroup?> GetByIdAsync(Guid id) =>
        Task.FromResult(SeedSkillGroups.FirstOrDefault(s => s.Id == id));

    public Task<SkillGroup> CreateAsync(SkillGroupRequest request)
    {
        var group = new SkillGroup
        {
            Id = Guid.NewGuid(),
            Category = request.Category,
            Icon = request.Icon,
            Items = request.Items,
        };
        SeedSkillGroups.Add(group);
        return Task.FromResult(group);
    }

    public Task UpdateAsync(Guid id, SkillGroupRequest request)
    {
        var index = SeedSkillGroups.FindIndex(s => s.Id == id);
        if (index < 0)
        {
            throw new KeyNotFoundException($"Skill group '{id}' not found.");
        }

        SeedSkillGroups[index] = new SkillGroup
        {
            Id = id,
            Category = request.Category,
            Icon = request.Icon,
            Items = request.Items,
        };
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var removed = SeedSkillGroups.RemoveAll(s => s.Id == id);
        if (removed == 0)
        {
            throw new KeyNotFoundException($"Skill group '{id}' not found.");
        }
        return Task.CompletedTask;
    }
}
