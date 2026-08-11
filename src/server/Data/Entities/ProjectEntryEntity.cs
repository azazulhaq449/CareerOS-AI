namespace CareerOS.Server.Data.Entities;

public class ProjectEntryEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Organisation { get; set; }
    public required string Icon { get; set; }
    public required string Description { get; set; }
    public required List<string> Tags { get; set; }
    public int SortOrder { get; set; }
}
