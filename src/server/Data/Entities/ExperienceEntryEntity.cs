namespace CareerOS.Server.Data.Entities;

public class ExperienceEntryEntity
{
    public Guid Id { get; set; }
    public required string Company { get; set; }
    public required string Location { get; set; }
    public required string Role { get; set; }
    public required string Period { get; set; }
    public required string Projects { get; set; }
    public required List<string> Highlights { get; set; }
    public int SortOrder { get; set; }
}
