namespace CareerOS.Server.Data.Entities;

public class SkillGroupEntity
{
    public Guid Id { get; set; }
    public required string Category { get; set; }
    public required string Icon { get; set; }
    public required List<string> Items { get; set; }
    public int SortOrder { get; set; }
}
