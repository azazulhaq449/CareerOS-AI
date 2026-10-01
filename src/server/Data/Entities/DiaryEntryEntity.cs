namespace CareerOS.Server.Data.Entities;

public class DiaryEntryEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Category { get; set; }
    public required List<string> Tags { get; set; }
    public DateOnly EntryDate { get; set; }
}
