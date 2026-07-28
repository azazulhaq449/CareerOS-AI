namespace CareerOS.Server.Models;

public class ProjectEntry
{
    public required string Name { get; init; }
    public required string Organisation { get; init; }
    public required string Icon { get; init; }
    public required string Description { get; init; }
    public required IReadOnlyList<string> Tags { get; init; }
}
