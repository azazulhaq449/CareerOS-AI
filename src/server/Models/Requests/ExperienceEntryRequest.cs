using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class ExperienceEntryRequest
{
    [Required] public required string Company { get; init; }
    [Required] public required string Location { get; init; }
    [Required] public required string Role { get; init; }
    [Required] public required string Period { get; init; }
    [Required] public required string Projects { get; init; }
    [Required] public required List<string> Highlights { get; init; }
    public int SortOrder { get; init; }
}
