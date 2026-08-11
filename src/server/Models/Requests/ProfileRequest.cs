using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class ProfileRequest
{
    [Required] public required string Name { get; init; }
    [Required] public required string Initials { get; init; }
    [Required] public required string Title { get; init; }
    [Required] public required List<string> Roles { get; init; }
    [Required] public required string Location { get; init; }
    [Required, EmailAddress] public required string Email { get; init; }
    [Required] public required string Phone { get; init; }
    [Required] public required string PhoneHref { get; init; }
    public string? LinkedInUrl { get; init; }
    [Required] public required List<string> Summary { get; init; }
}
