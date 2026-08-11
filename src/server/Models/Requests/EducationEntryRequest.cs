using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class EducationEntryRequest
{
    [Required] public required string School { get; init; }
    [Required] public required string Degree { get; init; }
    [Required] public required string Location { get; init; }
    [Required] public required string Date { get; init; }
}
