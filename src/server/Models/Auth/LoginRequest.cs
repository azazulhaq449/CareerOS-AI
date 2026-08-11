using System.ComponentModel.DataAnnotations;

namespace CareerOS.Server.Models.Auth;

public class LoginRequest
{
    [Required, EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
