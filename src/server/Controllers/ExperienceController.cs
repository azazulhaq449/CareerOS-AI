using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperienceController(IExperienceService experienceService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExperienceEntry>>> Get()
    {
        return Ok(await experienceService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExperienceEntry>> GetById(Guid id)
    {
        var entry = await experienceService.GetByIdAsync(id);
        return entry is null ? NotFound() : Ok(entry);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ExperienceEntry>> Create(ExperienceEntryRequest request)
    {
        var entry = await experienceService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ExperienceEntryRequest request)
    {
        await experienceService.UpdateAsync(id, request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await experienceService.DeleteAsync(id);
        return NoContent();
    }
}
