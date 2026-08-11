using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class StrengthRequest
{
    [Required] public required string Icon { get; init; }
    [Required] public required string Title { get; init; }
    [Required] public required string Description { get; init; }
    public int SortOrder { get; init; }
}
