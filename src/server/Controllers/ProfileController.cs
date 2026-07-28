using CareerOS.Server.Models;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController(IProfileService profileService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Profile>> Get()
    {
        return Ok(await profileService.GetProfileAsync());
    }

    [HttpGet("strengths")]
    public async Task<ActionResult<IReadOnlyList<Strength>>> GetStrengths()
    {
        return Ok(await profileService.GetStrengthsAsync());
    }
}
