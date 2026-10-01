using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class DiaryEntryRequest
{
    [Required] public required string Title { get; init; }
    [Required] public required string Content { get; init; }
    [Required] public required string Category { get; init; }
    [Required] public required List<string> Tags { get; init; }
    public DateOnly EntryDate { get; init; }
}
