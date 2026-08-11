namespace CareerOS.Server.Models;

public class Profile
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Initials { get; init; }
    public required string Title { get; init; }
    public required IReadOnlyList<string> Roles { get; init; }
    public required string Location { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string PhoneHref { get; init; }
    public string? LinkedInUrl { get; init; }
    public required IReadOnlyList<string> Summary { get; init; }
}
