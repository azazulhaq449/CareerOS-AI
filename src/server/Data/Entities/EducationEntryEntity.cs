namespace CareerOS.Server.Data.Entities;

public class EducationEntryEntity
{
    public Guid Id { get; set; }
    public required string School { get; set; }
    public required string Degree { get; set; }
    public required string Location { get; set; }
    public required string Date { get; set; }
}
