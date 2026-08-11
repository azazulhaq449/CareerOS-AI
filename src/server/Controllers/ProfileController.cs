using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update(ProfileRequest request)
    {
        await profileService.UpdateProfileAsync(request);
        return NoContent();
    }

    [HttpGet("strengths")]
    public async Task<ActionResult<IReadOnlyList<Strength>>> GetStrengths()
    {
        return Ok(await profileService.GetStrengthsAsync());
    }

    [Authorize]
    [HttpPost("strengths")]
    public async Task<ActionResult<Strength>> CreateStrength(StrengthRequest request)
    {
        var strength = await profileService.CreateStrengthAsync(request);
        return CreatedAtAction(nameof(GetStrengths), new { }, strength);
    }

    [Authorize]
    [HttpPut("strengths/{id:guid}")]
    public async Task<IActionResult> UpdateStrength(Guid id, StrengthRequest request)
    {
        await profileService.UpdateStrengthAsync(id, request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("strengths/{id:guid}")]
    public async Task<IActionResult> DeleteStrength(Guid id)
    {
        await profileService.DeleteStrengthAsync(id);
        return NoContent();
    }
}
