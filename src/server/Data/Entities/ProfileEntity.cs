namespace CareerOS.Server.Data.Entities;

public class ProfileEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Initials { get; set; }
    public required string Title { get; set; }
    public required List<string> Roles { get; set; }
    public required string Location { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string PhoneHref { get; set; }
    public string? LinkedInUrl { get; set; }
    public required List<string> Summary { get; set; }
}
