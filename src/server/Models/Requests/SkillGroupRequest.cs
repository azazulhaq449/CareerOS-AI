using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class SkillGroupRequest
{
    [Required] public required string Category { get; init; }
    [Required] public required string Icon { get; init; }
    [Required] public required List<string> Items { get; init; }
    public int SortOrder { get; init; }
}
