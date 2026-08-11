namespace CareerOS.Server.Data.Entities;

public class CertificationEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Date { get; set; }
    public int SortOrder { get; set; }
}
