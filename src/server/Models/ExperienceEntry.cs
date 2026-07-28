namespace CareerOS.Server.Models;

public class ExperienceEntry
{
    public required string Company { get; init; }
    public required string Location { get; init; }
    public required string Role { get; init; }
    public required string Period { get; init; }
    public required string Projects { get; init; }
    public required IReadOnlyList<string> Highlights { get; init; }
}
