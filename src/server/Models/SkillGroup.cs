namespace CareerOS.Server.Models;

public class SkillGroup
{
    public required Guid Id { get; init; }
    public required string Category { get; init; }
    public required string Icon { get; init; }
    public required IReadOnlyList<string> Items { get; init; }
}
