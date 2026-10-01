namespace CareerOS.Server.Models;

public class DiaryEntry
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required string Category { get; init; }
    public required IReadOnlyList<string> Tags { get; init; }
    public required DateOnly EntryDate { get; init; }
}
