namespace CareerOS.Server.Data.Entities;

public class StrengthEntity
{
    public Guid Id { get; set; }
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int SortOrder { get; set; }
}
