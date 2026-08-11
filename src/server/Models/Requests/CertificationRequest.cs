using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Requests;

public class CertificationRequest
{
    [Required] public required string Name { get; init; }
    [Required] public required string Date { get; init; }
    public int SortOrder { get; init; }
}
