using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class ProjectEntryRequest
{
    [Required] public required string Name { get; init; }
    [Required] public required string Organisation { get; init; }
    [Required] public required string Icon { get; init; }
    [Required] public required string Description { get; init; }
    [Required] public required List<string> Tags { get; init; }
    public int SortOrder { get; init; }
}
